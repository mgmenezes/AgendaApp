import { describe, it, expect, vi, beforeEach } from 'vitest'
import { mount } from '@vue/test-utils'
import ContatosList from '../ContatosList.vue'
import { ContatoService } from '../../services/ContatoService'
import PrimeVue from 'primevue/config'
import ToastService from 'primevue/toastservice'

vi.mock('vue-router', () => ({
  useRouter: () => ({
    push: vi.fn()
  })
}))

vi.mock('primevue/usetoast', () => ({
  useToast: () => ({
    add: vi.fn()
  })
}))

describe('ContatosList', () => {
  const mockContatos = [
    {
      id: '1',
      nome: 'João Silva',
      email: 'joao@email.com',
      telefone: '(11) 99999-9999',
      dataCriacao: new Date(),
      dataAtualizacao: null,
      ativo: true
    },
    {
      id: '2',
      nome: 'Maria Santos',
      email: 'maria@email.com',
      telefone: '(11) 88888-8888'
    }
  ]

  const mockContato = {
    id: '1',
    nome: 'João Silva',
    email: 'joao@email.com',
    telefone: '(11) 99999-9999',
    dataCriacao: new Date(),
    dataAtualizacao: undefined,
    ativo: true
  };

  const mountOptions = {
    global: {
      plugins: [PrimeVue, ToastService]
    }
  }

  beforeEach(() => {
    vi.clearAllMocks()
  })

  it('deve carregar e exibir a lista de contatos', async () => {
    vi.spyOn(ContatoService, 'listar').mockResolvedValue([mockContato])

    const wrapper = mount(ContatosList, mountOptions)
    
    // Aguarda a renderização
    await wrapper.vm.$nextTick()
    // Aguarda as promises serem resolvidas
    await new Promise(resolve => setTimeout(resolve, 0))
    // Força nova renderização
    await wrapper.vm.$nextTick()

    expect(wrapper.text()).toContain('João Silva')
  })

  it('deve exibir mensagem de erro quando falhar ao carregar contatos', async () => {
    vi.spyOn(ContatoService, 'listar').mockRejectedValue(new Error('Erro ao carregar'))

    const wrapper = mount(ContatosList, mountOptions)
    
    await wrapper.vm.$nextTick()
   
    await new Promise(resolve => setTimeout(resolve, 0))
    
    await wrapper.vm.$nextTick()

    
    expect(wrapper.find('.error-message').text()).toContain('Não foi possível carregar os contatos')
  })

  it('deve inativar um contato com sucesso', async () => {
    vi.spyOn(ContatoService, 'listar').mockResolvedValue([mockContato])
    const mockInativar = vi.spyOn(ContatoService, 'inativar').mockResolvedValue(undefined)

    const wrapper = mount(ContatosList, mountOptions)

    
    await wrapper.vm.$nextTick()
   
    await new Promise(resolve => setTimeout(resolve, 0))
    
    await wrapper.vm.$nextTick()

    
    const inativarButton = wrapper.find('.button-danger')
    expect(inativarButton.exists()).toBe(true)
    await inativarButton.trigger('click')

    
    expect(mockInativar).toHaveBeenCalledWith('1')
  })
}) 