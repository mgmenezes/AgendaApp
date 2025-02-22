// src/App.vue
<script setup lang="ts">

import DefaultLayout from '@/layouts/DefaultLayout.vue'

import './assets/main.css'
</script>

<template>
  <DefaultLayout>
    <RouterView />
  </DefaultLayout>
</template>