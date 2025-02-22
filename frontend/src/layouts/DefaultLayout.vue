<script setup lang="ts">
</script>

<template>
  <div class="layout-container">
    <header class="header">
      <!-- <nav class="nav-container">
        <h1 class="app-title">Agenda App</h1>
      </nav> -->
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
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: white;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  border-radius: 0.5rem;
}

/* Container da navegação */
.nav-container {
  width: 50rem; 
  display: flex;
  align-items: center;
  justify-content: center;
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