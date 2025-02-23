<script setup lang="ts">
import { ref, onMounted, computed } from "vue";
import { useRouter, useRoute } from "vue-router";
import { useVuelidate } from "@vuelidate/core";
import { required, email, helpers } from "@vuelidate/validators";
import { ContatoService } from "../services/ContatoService";
import type { ContatoInput } from "../types/Contato";
import { useToast } from "primevue/usetoast";
import Toast from "primevue/toast";
import ProgressSpinner from "primevue/progressspinner";

const initialState: ContatoInput = {
  id: "",
  nome: "",
  email: "",
  telefone: "",
};

const formData = ref<ContatoInput>({ ...initialState });

const rules = {
  nome: {
    required: helpers.withMessage("Nome é obrigatório", required),
    minLength: helpers.withMessage(
      "Nome deve ter pelo menos 10 caracteres",
      (value: string) => value.length >= 10
    ),
  },
  email: {
    required: helpers.withMessage("Email é obrigatório", required),
    email: helpers.withMessage("Email inválido", email),
  },
  telefone: {
    required: helpers.withMessage("Telefone é obrigatório", required),
    format: helpers.withMessage(
      "Telefone deve estar no formato (99) 99999-9999",
      (value: string) => /^\(\d{2}\) \d{5}-\d{4}$/.test(value)
    ),
  },
};

const v$ = useVuelidate(rules, formData);

const router = useRouter();
const route = useRoute();
const id = route.params.id as string;

const isEditMode = computed(() => route.params.id !== undefined);

const toast = useToast();

const isLoading = ref(false);
const errorMessage = ref<string | null>(null);

async function loadContato() {
  if (id) {
    try {
      isLoading.value = true;
      const contato = await ContatoService.obterPorId(id);

      if (!contato) {
        throw new Error("Contato não encontrado");
      }

      formData.value = {
        id: contato.id,
        nome: contato.nome,
        email: contato.email,
        telefone: contato.telefone,
      };
    } catch (error) {
      console.error("Erro ao carregar contato:", error);
      toast.add({
        severity: "error",
        summary: "Erro",
        detail: "Não foi possível carregar os dados do contato",
        life: 3000,
      });

      setTimeout(() => {
        router.push("/agenda");
      }, 2000);
    } finally {
      isLoading.value = false;
    }
  }
}

const handleSubmit = async () => {
  const isValid = await v$.value.$validate();
  if (!isValid) {
    toast.add({
      severity: "error",
      summary: "Erro de validação",
      detail: "Por favor, verifique os campos obrigatórios",
      life: 3000,
    });
    return;
  }

  try {
    if (id) {
      await ContatoService.atualizar(id, formData.value);
      toast.add({
        severity: "success",
        summary: "Sucesso",
        detail: "Contato atualizado com sucesso!",
        life: 3000,
      });
    } else {
      await ContatoService.criar(formData.value);
      toast.add({
        severity: "success",
        summary: "Sucesso",
        detail: "Contato cadastrado com sucesso!",
        life: 3000,
      });
    }

    setTimeout(() => {
      router.push("/agenda");
    }, 1000);
  } catch (error) {
    console.error("Erro ao salvar contato:", error);
    toast.add({
      severity: "error",
      summary: "Erro",
      detail: "Erro ao salvar o contato. Tente novamente.",
      life: 3000,
    });
  }
};

function formatarTelefone(value: string): string {
  // Remove tudo que não for número
  const numbers = value.replace(/\D/g, "");

  // Limita a 11 dígitos
  const limitedNumbers = numbers.slice(0, 11);

  // Aplica a máscara
  if (limitedNumbers.length <= 2) {
    return `(${limitedNumbers}`;
  }
  if (limitedNumbers.length <= 7) {
    return `(${limitedNumbers.slice(0, 2)}) ${limitedNumbers.slice(2)}`;
  }
  return `(${limitedNumbers.slice(0, 2)}) ${limitedNumbers.slice(
    2,
    7
  )}-${limitedNumbers.slice(7)}`;
}

onMounted(() => {
  loadContato();
});
</script>

<template>
  <div class="card p-4">
    <Toast />

    <!-- Loading state -->
    <div v-if="isLoading" class="flex justify-center items-center h-64">
      <ProgressSpinner />
    </div>

    <!-- Form content -->
    <div v-else>
      <h1 class="text-2xl mb-4">
        {{ isEditMode ? "Editar Contato" : "Novo Contato" }}
      </h1>

      <form @submit.prevent="handleSubmit" class="max-w-lg">
        <div class="field mb-4">
          <label for="nome" class="block mb-2">Nome</label>
          <PInputText
            id="nome"
            v-model="formData.nome"
            :class="{ 'p-invalid': v$.nome.$error }"
            class="w-full"
          />
          <small class="p-error" v-if="v$.nome.$error">
            {{ v$.nome.$errors[0].$message }}
          </small>
        </div>

        <div class="field mb-4">
          <label for="email" class="block mb-2">Email</label>
          <PInputText
            id="email"
            v-model="formData.email"
            :class="{ 'p-invalid': v$.email.$error }"
            class="w-full"
          />
          <small class="p-error" v-if="v$.email.$error">
            {{ v$.email.$errors[0].$message }}
          </small>
        </div>

        <div class="field mb-4">
          <label for="telefone" class="block mb-2">Telefone</label>
          <PInputText
            id="telefone"
            v-model="formData.telefone"
            placeholder="(99) 99999-9999"
            maxlength="15"
            :class="{ 'p-invalid': v$.telefone.$error }"
            class="w-full"
            @input="formData.telefone = formatarTelefone($event.target.value)"
          />
          <small class="p-error" v-if="v$.telefone.$error">
            {{ v$.telefone.$errors[0].$message }}
          </small>
        </div>

        <div class="flex gap-2">
          <PButton
            type="submit"
            label="Salvar"
            icon="pi pi-check"
            class="p-button-primary"
          />
          <PButton
            type="button"
            label="Cancelar"
            icon="pi pi-times"
            class="p-button-secondary"
            @click="router.push('/agenda')"
          />
        </div>
      </form>
    </div>
  </div>
</template>
