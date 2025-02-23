import { config } from '@vue/test-utils'
import PrimeVue from 'primevue/config'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import Toast from 'primevue/toast'
import ToastService from 'primevue/toastservice'

config.global.plugins = [PrimeVue, ToastService]
config.global.components = {
  'PButton': Button,
  'PInputText': InputText,
  'Toast': Toast
} 