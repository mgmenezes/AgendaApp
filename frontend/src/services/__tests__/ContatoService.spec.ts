import { describe, it, expect, vi, beforeEach } from 'vitest'
import { ContatoService } from '../ContatoService'
import { api } from '../Api'

// Mock do módulo Api
vi.mock('../Api', () => ({
  api: {
    get: vi.fn(),
    post: vi.fn(),
    put: vi.fn(),
    delete: vi.fn()
  }
}))

describe('ContatoService', () => {
  const mockContato = {
    id: '1',
    nome: 'João Silva',
    email: 'joao@email.com',
    telefone: '(11) 99999-9999',
    dataCriacao: new Date(),
    dataAtualizacao: undefined,
    ativo: true
  };

  beforeEach(() => {
    vi.clearAllMocks()
  })

  it('deve listar contatos corretamente', async () => {
    vi.mocked(api.get).mockResolvedValue({ data: [mockContato] })

    const result = await ContatoService.listar()
    expect(result).toEqual([mockContato])
  })

  it('deve inativar contato corretamente', async () => {
    vi.mocked(api.delete).mockResolvedValue({ status: 204 })

    await ContatoService.inativar('1')
    expect(api.delete).toHaveBeenCalledWith('/contatos/1', expect.any(Object))
  })

  it('deve lançar erro quando a requisição falhar', async () => {
    vi.mocked(api.get).mockRejectedValue(new Error('Erro na requisição'))

    await expect(ContatoService.listar()).rejects.toThrow('Erro na requisição')
  })
}) 