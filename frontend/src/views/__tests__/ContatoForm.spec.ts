import { describe, it, expect, vi, beforeEach } from 'vitest'
import { mount } from '@vue/test-utils'
import { createTestingPinia } from '@pinia/testing'
import ContatoForm from '../ContatoForm.vue'
import { ContatoService } from '../../services/ContatoService'
import PrimeVue from 'primevue/config'
import ToastService from 'primevue/toastservice'

vi.mock('vue-router', () => ({
  useRouter: () => ({
    push: vi.fn()
  }),
  useRoute: () => ({
    params: {
      id: undefined
    }
  })
}))

vi.mock('primevue/usetoast', () => ({
  useToast: () => ({
    add: vi.fn()
  })
}))

describe('ContatoForm', () => {
  beforeEach(() => {
    vi.clearAllMocks()
  })

  const mountOptions = {
    global: {
      plugins: [
        createTestingPinia(),
        PrimeVue,
        ToastService
      ]
    }
  }

  it('deve validar campos obrigatórios', async () => {
    const wrapper = mount(ContatoForm, mountOptions)

    await wrapper.find('form').trigger('submit')
    
    expect(wrapper.text()).toContain('Nome é obrigatório')
    expect(wrapper.text()).toContain('Email é obrigatório')
  })

  it('deve criar um novo contato com sucesso', async () => {
    const mockContato = {
      id: '1',
      nome: 'João da Silva',
      email: 'joao@email.com',
      telefone: '(11) 99999-9999',
      dataCriacao: new Date(),
      dataAtualizacao: undefined,
      ativo: true
    };
    
    const mockCriar = vi.spyOn(ContatoService, 'criar').mockResolvedValue(mockContato);
    
    const wrapper = mount(ContatoForm, mountOptions);

    // Preenche os campos
    await wrapper.find('input[id="nome"]').setValue('João da Silva');
    await wrapper.find('input[id="email"]').setValue('joao@email.com');
    await wrapper.find('input[id="telefone"]').setValue('(11) 99999-9999');

    // Aguarda a validação
    await wrapper.vm.$nextTick();

    // Submete o formulário
    const form = wrapper.find('form');
    await form.trigger('submit.prevent');
    
    // Aguarda o processamento
    await wrapper.vm.$nextTick();
    await new Promise(resolve => setTimeout(resolve, 0));

    // Verifica se o método foi chamado com os dados corretos
    expect(mockCriar).toHaveBeenCalledWith({
      id: '',
      nome: 'João da Silva',
      email: 'joao@email.com',
      telefone: '(11) 99999-9999'
    });
  })
}) 