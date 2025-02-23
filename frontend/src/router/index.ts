// src/router/index.ts
import {
  createRouter,
  createWebHistory,
  type RouteRecordRaw,
} from "vue-router";
import ContatosList from "@/views/ContatosList.vue";
import ContatoForm from "@/views/ContatoForm.vue";
import HomePage from "@/views/HomePage.vue";
import LoginView from "@/views/LoginView.vue";
import { AuthService } from "@/services/auth/authService";

const routes = [
  {
    path: "/login",
    name: "login",
    component: LoginView,
  },
  {
    path: "/",
    name: "home",
    component: HomePage,
  },
  {
    path: "/agenda",
    name: "agenda",
    component: ContatosList,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/contato/novo",
    name: "novo-contato",
    component: ContatoForm,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/contato/:id",
    name: "editar-contato",
    component: ContatoForm,
    meta: {
      requiresAuth: true,
    },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  if (to.meta.requiresAuth && !AuthService.isAuthenticated()) {
    next('/login');
  } else {
    next();
  }
});

export default router;
