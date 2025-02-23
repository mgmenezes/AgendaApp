// src/views/HomePage.vue
<script setup lang="ts">
import { useRouter } from 'vue-router';

const router = useRouter();

const openGitHubRepo = () => {
  window.open('https://github.com/mgmenezes/AgendaApp', '_blank');
};
</script>



<template>
  <div class="home-container">
    <div class="content">
      <nav class="navigation">
        <a href="#" class="nav-link active">Apresentação</a>
        <a href="#" class="nav-link" @click="router.push('/agenda')">Agenda</a>
      </nav>

      <div class="main-content">
        <div class="tech-description">
          <h2 class="description">Desafio Blue Technology utilizando</h2>
          <div class="tech-icons">
            <img src="/dotnet-logo.svg" alt=".NET" class="tech-icon dotnet" />
            <span class="tech-separator">+</span>
            <img src="/vue-logo.svg" alt="Vue.js" class="tech-icon vue" />
          </div>
        </div>
        <a 
          href="https://github.com/mgmenezes/AgendaApp" 
          target="_blank" 
          rel="noopener noreferrer"
          class="github-container"
        >
    <img src="/github-logo.png" alt="GitHub Logo" class="github-logo" />
    <span class="github-tooltip">Clique para acessar o repo GitHub</span>
  </a>
      </div>
    </div>
  </div>
</template>

<style scoped>
.home-container {
  min-height: 100vh;
  background-color: white;
  padding: 20px;
}

.content {
  max-width: 1200px;
  margin: 0 auto;
  padding: 40px 20px;
  text-align: center;
}

.navigation {
  display: flex;
  justify-content: center;
  gap: 20px;
  margin-bottom: 60px;
}

.nav-link {
  text-decoration: none;
  color: #333;
  font-size: 16px;
  padding: 8px 16px;
  border-radius: 4px;
  transition: all 0.3s ease;
}

.nav-link:hover {
  color: #2563eb;
}

.nav-link.active {
  color: #10b981;
  font-weight: 500;
}

.main-content {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 40px;
}

.illustration {
  max-width: 400px;
  height: auto;
}

.description {
  font-size: 24px;
  color: #374151;
  max-width: 600px;
  line-height: 1.4;
}

.github-container {
  position: relative;
  cursor: pointer;
  padding: 10px;
  transition: transform 0.3s ease;
}

.github-container:hover {
  transform: scale(1.1);
}

.github-container:hover .github-tooltip {
  opacity: 1;
  visibility: visible;
  transform: translateY(0);
}

.github-logo {
  width: 100px;
  height: auto;
  margin-top: 20px;
  /* Adicionando um sutil efeito de sombra */
  filter: drop-shadow(0 2px 4px rgba(0, 0, 0, 0.1));
}

.github-tooltip {
  position: absolute;
  bottom: -50px;
  left: -50%;
  transform: translateX(-500%) translateY(10px);
  background-color: #333;
  color: white;
  padding: 8px 12px;
  border-radius: 6px;
  font-size: 14px;
  white-space: nowrap;
  opacity: 0;
  visibility: hidden;
  transition: all 0.3s ease;
}

/* Criando a seta do tooltip */
.github-tooltip::before {
  content: '';
  position: absolute;
  top: -6px;
  left: 50%;
  transform: translateX(-100%);
  border-width: 0 6px 6px 6px;
  border-style: solid;
  border-color: transparent transparent #333 transparent;
}

/* Melhorando a acessibilidade com foco via teclado */
.github-container:focus-visible {
  outline: 2px solid #2563eb;
  border-radius: 4px;
}

/* Adicionando animação suave na ilustração */
.illustration {
  max-width: 400px;
  height: auto;
  transition: transform 0.5s ease;
}

.illustration:hover {
  transform: translateY(-5px);
}

/* Responsividade ajustada */
@media (max-width: 768px) {
  .github-tooltip {
    font-size: 12px;
    padding: 6px 10px;
  }
  
  .illustration {
    max-width: 280px;
  }
}

.tech-description {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 20px;
}

.tech-icons {
  display: flex;
  align-items: center;
  gap: 24px;
  margin-top: 10px;
}

.tech-icon {
  width: 60px;
  height: 60px;
  object-fit: contain;
  transition: transform 0.3s ease;
}

.tech-icon:hover {
  transform: scale(1.1);
}

.tech-icon.dotnet {
  filter: brightness(0) saturate(100%) invert(31%) sepia(98%) 
          saturate(1720%) hue-rotate(181deg) brightness(96%) contrast(101%);
}

.tech-icon.vue {
  filter: brightness(0) saturate(100%) invert(48%) sepia(47%) 
          saturate(875%) hue-rotate(127deg) brightness(95%) contrast(101%);
}

.tech-separator {
  font-size: 24px;
  font-weight: bold;
  color: #666;
}

/* Ajuste responsivo */
@media (max-width: 768px) {
  .tech-icons {
    gap: 16px;
  }

  .tech-icon {
    width: 40px;
    height: 40px;
  }

  .tech-separator {
    font-size: 20px;
  }
}
</style>