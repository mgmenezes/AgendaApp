import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import ContatoCard from '../ContatoCard.vue'

describe('ContatoCard', () => {
  const mockContato = {
    id: '1',
    nome: 'João Silva',
    email: 'joao@email.com',
    telefone: '(11) 99999-9999',
    dataCriacao: new Date().toISOString(),
    dataAtualizacao: null,
    ativo: true
  }

  it('deve renderizar as informações do contato corretamente', () => {
    const wrapper = mount(ContatoCard, {
      props: {
        contato: mockContato
      }
    })

    expect(wrapper.text()).toContain('João Silva')
    expect(wrapper.text()).toContain('joao@email.com')
    expect(wrapper.text()).toContain('(11) 99999-9999')
  })

  it('deve emitir evento "edit" ao clicar no botão editar', async () => {
    const wrapper = mount(ContatoCard, {
      props: {
        contato: mockContato
      }
    })

    await wrapper.find('button:first-child').trigger('click')
    expect(wrapper.emitted('edit')).toBeTruthy()
  })

  it('deve emitir evento "inativar" ao clicar no botão inativar', async () => {
    const wrapper = mount(ContatoCard, {
      props: {
        contato: mockContato
      }
    })

    await wrapper.find('button:last-child').trigger('click')
    expect(wrapper.emitted('inativar')).toBeTruthy()
  })
}) 