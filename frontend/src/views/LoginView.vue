// src/views/LoginView.vue
<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const email = ref('')
const password = ref('')
const errorMessage = ref('')
const isLoading = ref(false)

const handleLogin = async () => {
  try {
    isLoading.value = true
    errorMessage.value = ''

    // Aqui vamos implementar a chamada para a API de autenticação
    const response = await fetch('http://localhost:5162/api/auth/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        email: email.value,
        password: password.value,
      }),
    })

    if (!response.ok) {
      throw new Error('Credenciais inválidas')
    }

    const data = await response.json()
    // Armazenar o token
    localStorage.setItem('token', data.token)
    
    // Redirecionar para a agenda
    router.push('/agenda')
  } catch (error) {
    errorMessage.value = 'Email ou senha incorretos'
  } finally {
    isLoading.value = false
  }
}
</script>

<template>
  <div class="login-container">
    <div class="login-card">
      <h1 class="login-title">Login</h1>
      
      <form @submit.prevent="handleLogin" class="login-form">
        <div class="form-group">
          <label for="email">Email</label>
          <input 
            id="email"
            v-model="email"
            type="email"
            required
            placeholder="seu@email.com"
            class="input-field"
          />
        </div>

        <div class="form-group">
          <label for="password">Senha</label>
          <input 
            id="password"
            v-model="password"
            type="password"
            required
            placeholder="Sua senha"
            class="input-field"
          />
        </div>

        <div v-if="errorMessage" class="error-message">
          {{ errorMessage }}
        </div>

        <button 
          type="submit" 
          class="login-button"
          :disabled="isLoading"
        >
          {{ isLoading ? 'Carregando...' : 'Entrar' }}
        </button>
      </form>
    </div>
  </div>
</template>

<style scoped>
.login-container {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: #f3f4f6;
}

.login-card {
  background: white;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 400px;
}

.login-title {
  text-align: center;
  font-size: 1.5rem;
  font-weight: 600;
  margin-bottom: 1.5rem;
  color: #374151;
}

.login-form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.form-group label {
  font-size: 0.875rem;
  color: #4b5563;
}

.input-field {
  padding: 0.5rem;
  border: 1px solid #d1d5db;
  border-radius: 4px;
  font-size: 1rem;
  transition: border-color 0.2s;
}

.input-field:focus {
  border-color: #3b82f6;
  outline: none;
  box-shadow: 0 0 0 2px rgba(59, 130, 246, 0.1);
}

.login-button {
  background-color: #3b82f6;
  color: white;
  padding: 0.75rem;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.2s;
}

.login-button:hover {
  background-color: #2563eb;
}

.login-button:disabled {
  background-color: #9ca3af;
  cursor: not-allowed;
}

.error-message {
  color: #dc2626;
  font-size: 0.875rem;
  text-align: center;
  padding: 0.5rem;
  background-color: #fee2e2;
  border-radius: 4px;
}
</style>