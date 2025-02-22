
<script setup lang="ts">
import type { Contato } from '../types/Contato'

defineProps<{
  contato: Contato
}>()

const emit = defineEmits<{
  (e: 'edit'): void
  (e: 'inativar'): void
}>()
</script>

<template>
  <div class="bg-white p-4 rounded-lg shadow hover:shadow-md transition-shadow">
    <div class="mb-2 text-overflow-ellipsis">
      <h3 class="text-lg font-semibold">{{ contato.nome }}</h3>
      <p class="text-gray-600">{{ contato.email }}</p>
      <p class="text-gray-600">{{ contato.telefone }}</p>
    </div>
    
    <div class="flex gap-2 mt-4">
      <button
        @click="emit('edit')"
        class="px-3 py-1 bg-blue-500 text-white rounded hover:bg-blue-600"
      >
        Editar
      </button>
      <button
        @click="emit('inativar')"
        class="px-3 py-1 bg-red-500 text-white rounded hover:bg-red-600"
      >
        Inativar
      </button>
    </div>
  </div>
</template>