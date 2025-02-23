<script setup lang="ts">
import { useRouter } from 'vue-router';
import { AuthService } from '@/services/auth/authService';
const handleLogout = () => {
  AuthService.removeToken(); // Remove o token do localStorage
  router.push('/login'); // Redireciona para a página de login
};

const router = useRouter();

router.push('/login');
</script>

<template>
  <div class="layout-container">
    <header class="header">
      <nav class="nav-container">
        <PButton
        v-if="AuthService.isAuthenticated()" 
          @click="handleLogout" 
          class="logout-button">Sair</PButton>
      </nav>
    </header>

    <main class="main-container">
      <slot></slot>
    </main>
  </div>
</template>

<style scoped>
/* Container principal do layout */
.layout-container {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  width: 60rem;
  background-color: #f3f4f6;
}

/* Estilização do header */
.header {
  width: 59rem;	
  display: flex;
  align-items: flex-end;
  justify-content: flex-end;
  border-radius: 0.5rem;
}

/* Container da navegação */
.nav-container {
  width: 50rem; 
  display: flex;
  justify-content: flex-end;
  align-items: center;
}

/* Título da aplicação */
.app-title {
  font-size: 1.5rem; /* Equivalente ao text-4xl */
  font-weight: 600;
  color: #111827; /* Equivalente ao text-gray-900 */
}

/* Container principal do conteúdo */
.main-container {
  width: 50rem;
  margin: 0 auto;
  padding: 24px 16px; /* Equivalente ao py-6 */
  width: 100%;
  flex: 1;
}
.logout-button {
  background-color: #ef4444;
  color: white;
  padding: 8px 16px;
  border: none;
  border-radius: 4px;
  font-size: 14px;
  cursor: pointer;
  transition: background-color 0.2s;
}

.logout-button:hover {
  background-color: #dc2626;
}

/* Media queries para responsividade */
@media (min-width: 640px) { /* sm breakpoint */
  .nav-container,
  .main-container {
    padding-left: 24px;
    padding-right: 24px;
  }
}

@media (min-width: 1024px) { /* lg breakpoint */
  .nav-container,
  .main-container {
    padding-left: 32px;
    padding-right: 32px;
  }
}
</style>