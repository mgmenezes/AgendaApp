// src/views/ContatosList.vue
<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { ContatoService } from '../services/ContatoService'
import type { Contato } from '../types/Contato'
// import Button from 'primevue/button';
import ProgressSpinner from 'primevue/progressspinner';
import { useToast } from 'primevue/usetoast'
import Toast from 'primevue/toast'

const contatos = ref<Contato[]>([])
const router = useRouter()
const isLoading = ref(true)
const errorMessage = ref<string | null>(null)
const toast = useToast()

async function loadContatos() {
  try {
    isLoading.value = true
    errorMessage.value = null
    contatos.value = await ContatoService.listar()
  } catch (error) {
    errorMessage.value = 'Não foi possível carregar os contatos. Por favor, verifique sua conexão e tente novamente.'
    console.error('Erro ao carregar contatos:', error)
  } finally {
    isLoading.value = false
  }
}

const handleInativar = async (id: string) => {
  try {
    await ContatoService.inativar(id)
    await loadContatos()
    toast.add({
      severity: 'success',
      summary: 'Sucesso',
      detail: 'Contato removido com sucesso!',
      life: 3000
    })
  } catch (error) {
    console.error('Erro ao inativar contato:', error)
    toast.add({
      severity: 'error',
      summary: 'Erro',
      detail: 'Não foi possível remover o contato. Tente novamente.',
      life: 3000
    })
  }
}

onMounted(loadContatos)
</script>

<template>
  <div class="container">
    <Toast />
    <!-- Header com título e botão -->
    <div class="header">
      <h1 class="title">Agenda de Contatos</h1>
      <PButton 
        class="button button-primary"
        @click="router.push('/contato/novo')"
      >
        + Novo Contato
      </PButton>
    </div>

    <!-- Container principal -->
    <div class="main-content">
      <!-- Estado de carregamento -->
      <div v-if="isLoading" class="loading-state">
        <ProgressSpinner />
        <p class="loading-text">Carregando seus contatos...</p>
      </div>

      <!-- Estado de erro -->
      <div v-else-if="errorMessage" class="error-container">
        <div class="error-content">
          <span class="error-icon">⚠</span>
          <p class="error-message">{{ errorMessage }}</p>
          <PButton 
            class="button button-retry"
            @click="loadContatos"
          >
            Tentar Novamente
          </PButton>
        </div>
      </div>

      <!-- Lista de contatos -->
      <div v-else class="contacts-grid">
        <div v-for="contato in contatos" 
             :key="contato.id" 
             class="contact-card">
          <div class="card-header">
            <h3 class="contact-name">{{ contato.nome }}</h3>
          </div>
          <div class="card-content">
            <div class="contact-info">
              <i class="pi pi-envelope"></i>
              <span>{{ contato.email }}</span>
            </div>
            <div class="contact-info">
              <i class="pi pi-phone"></i>
              <span>{{ contato.telefone }}</span>
            </div>
          </div>
          <div class="card-actions">
            <button 
              class="button button-secondary"
              @click="router.push(`/contato/${contato.id}`)"
            >
              Editar
            </button>
            <PButton 
              class="button button-danger"
              @click="handleInativar(contato.id)"
            >
              Inativar
            </PButton>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Container principal */
.container {
  margin: 0 auto;
  padding: 20px;
}

/* Estilização do header */
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 30px;
  padding: 20px;
  background-color: white;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.title {
  font-size: 24px;
  font-weight: bold;
  color: #333;
}

/* Container principal de conteúdo */
.main-content {
  width: 100%;
  min-height: 600px;
  background-color: white;
  border-radius: 8px;
  padding: 20px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

/* Estado de carregamento */
.loading-state {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 400px;
}

.loading-text {
  margin-top: 16px;
  color: #666;
}

/* Estado de erro */
.error-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 400px;
  padding: 20px;
}

.error-content {
  display: flex;
  align-items: center;
  padding: 20px;
  background-color: #fee2e2;
  border-left: 4px solid #ef4444;
  border-radius: 8px;
  max-width: 700px;
  width: 100%;
}

.error-icon {
  font-size: 24px;
  color: #ef4444;
  margin-right: 16px;
}

.error-message {
  color: #991b1b;
  flex-grow: 1;
  margin: 0;
}

/* Grid de contatos */
.contacts-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 20px;
}

/* Card de contato */
.contact-card {
  background-color: white;
  border-radius: 8px;
  border: 1px solid #e5e7eb;
  transition: box-shadow 0.3s ease;
}

.contact-card:hover {
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.card-header {
  padding: 16px;
  border-bottom: 1px solid #e5e7eb;
}

.contact-name {
  font-size: 18px;
  font-weight: 600;
  color: #333;
  margin: 0;
}

.card-content {
  padding: 16px;
}

.contact-info {
  display: flex;
  align-items: center;
  margin-bottom: 8px;
  text-overflow: ellipsis;
  overflow: hidden;
  white-space: nowrap;
}

.contact-info i {
  margin-right: 8px;
  color: #666;
}

.card-actions {
  display: flex;
  justify-content: flex-end;
  gap: 8px;
  padding: 16px;
  border-top: 1px solid #e5e7eb;
}

/* Botões */
.button {
  padding: 8px 16px;
  border-radius: 6px;
  font-weight: 500;
  cursor: pointer;
  border: none;
  transition: background-color 0.2s ease;
}
.button-retry {
  background-color: transparent;
  color: #ef4444;
  padding: 6px 12px;
}

.button-retry:hover {
  background-color: #fee2e2;
}

/* Media queries para responsividade */
@media (max-width: 768px) {
  .header {
    flex-direction: column;
    gap: 16px;
    text-align: center;
  }

  .contacts-grid {
    grid-template-columns: 1fr;
  }
}
</style>
