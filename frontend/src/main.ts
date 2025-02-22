// src/main.ts
import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import PrimeVue from "primevue/config";

import "./assets/main.css";
import "primevue/resources/themes/lara-light-blue/theme.css"; // tema
import "primevue/resources/primevue.min.css"; // core
import "primeicons/primeicons.css"; // ícones
import "primeflex/primeflex.css"; // sistema de grid (opcional, mas útil)

import Button from "primevue/button";
import InputText from "primevue/inputtext";
import Card from "primevue/card";
import Dialog from "primevue/dialog";
import Toast from "primevue/toast";
import ToastService from "primevue/toastservice";

const app = createApp(App);
app.component("PButton", Button);
app.component("PInputText", InputText);
app.component("PCard", Card);
app.component("PDialog", Dialog);
app.component("PToast", Toast);
app.component("PToastService", ToastService);

app.use(router);
app.use(PrimeVue, { ripple: true });
app.use(ToastService);

app.mount("#app");
