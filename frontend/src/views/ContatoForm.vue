<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { useVuelidate } from '@vuelidate/core'
import { required, email, helpers } from '@vuelidate/validators'
import { ContatoService } from '../services/ContatoService'
import type { ContatoInput } from '../types/Contato'
import { useToast } from 'primevue/usetoast'
import Toast from 'primevue/toast'

// Definindo o estado inicial do formulário
const initialState: ContatoInput = {
  id: '',
  nome: '',
  email: '',
  telefone: ''
}

// Criando referência reativa para os dados do formulário
const formData = ref<ContatoInput>({ ...initialState })

// Configurando as regras de validação
const rules = {
  nome: { 
    required: helpers.withMessage('Nome é obrigatório', required),
    minLength: helpers.withMessage(
      'Nome deve ter pelo menos 10 caracteres',
      (value: string) => value.length >= 10
    )
  },
  email: { 
    required: helpers.withMessage('Email é obrigatório', required),
    email: helpers.withMessage('Email inválido', email)
  },
  telefone: {
    // required: helpers.withMessage('Telefone é obrigatório', required),
    // format: helpers.withMessage(
    //   'Telefone deve estar no formato (99) 99999-9999',
    //   (value: string) => /^\(\d{2}\) \d{5}-\d{4}$/.test(value)
    // )
  }
}

// Instanciando o validador
const v$ = useVuelidate(rules, formData)

// Obtendo router e route para navegação e acesso aos parâmetros
const router = useRouter()
const route = useRoute()

// Verifica se estamos no modo de edição
const isEditMode = computed(() => route.params.id !== undefined)

// Após as outras constantes declaradas
const toast = useToast()

// Função para carregar os dados do contato em modo de edição
async function loadContato() {
  if (isEditMode.value) {
    try {
      const id = route.params.id as string
      const contato = await ContatoService.obterPorId(id)
      console.log('Contato carregado:', contato)
      formData.value = {
        id: contato.id,
        nome: contato.nome,
        email: contato.email,
        telefone: contato.telefone
      }
    } catch (error) {
      console.error('Erro ao carregar contato:', error)
      router.push('/')
    }
  }
}

async function handleSubmit() {
  const isValid = await v$.value.$validate()
  if (!isValid) {
    toast.add({
      severity: 'error',
      summary: 'Erro de validação',
      detail: 'Por favor, verifique os campos obrigatórios',
      life: 3000
    })
    return
  }
  
  try {
    if (isEditMode.value) {
      await ContatoService.atualizar(route.params.id as string, formData.value)
      toast.add({
        severity: 'success',
        summary: 'Sucesso',
        detail: 'Contato atualizado com sucesso!',
        life: 3000
      })
    } else {
      await ContatoService.criar(formData.value)
      toast.add({
        severity: 'success',
        summary: 'Sucesso',
        detail: 'Contato cadastrado com sucesso!',
        life: 3000
      })
    }
    
    // Adicionando um pequeno delay antes do redirecionamento
    setTimeout(() => {
      router.push('/agenda')
    }, 1000)
  } catch (error) {
    console.error('Erro ao salvar contato:', error)
    toast.add({
      severity: 'error',
      summary: 'Erro',
      detail: 'Erro ao salvar o contato. Tente novamente.',
      life: 3000
    })
  }
}

// Função para formatar o telefone
function formatarTelefone(value: string): string {
  // Remove todos os caracteres não numéricos
  const numbers = value.replace(/\D/g, '')
  
  // Aplica a máscara
  if (numbers.length <= 11) {
    return numbers.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3')
      .replace(/(\d{2})(\d{0,5})/, '($1) $2')
      .replace(/(\d{2})(\d{5})(\d{0,4})/, '($1) $2-$3')
      .trim()
  }
  return value
}

onMounted(loadContato)
</script>

<template>
  <div class="card p-4">
    <Toast />
    <h1 class="text-2xl mb-4">
      {{ isEditMode ? 'Editar Contato' : 'Novo Contato' }}
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
          placeholder="(99)99999-9999"
          maxlength="13"
          :class="{ 'p-invalid': v$.telefone.$error }"
          class="w-full"
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
          @click="router.push('/')"
        />
      </div>
    </form>
  </div>
</template>