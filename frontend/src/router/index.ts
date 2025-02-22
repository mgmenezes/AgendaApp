// src/router/index.ts
import {
  createRouter,
  createWebHistory,
  type RouteRecordRaw,
} from "vue-router";
import ContatosList from "@/views/ContatosList.vue";
import ContatoForm from "@/views/ContatoForm.vue";
import HomePage from "@/views/HomePage.vue";

const routes = [
  {
    path: "/",
    name: "home",
    component: HomePage,
  },
  {
    path: "/agenda",
    name: "agenda",
    component: ContatosList,
  },
  {
    path: "/contato/novo",
    name: "novo-contato",
    component: ContatoForm,
  },
  {
    path: "/contato/:id",
    name: "editar-contato",
    component: ContatoForm,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
