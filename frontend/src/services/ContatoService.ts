import type { Contato, ContatoInput } from "../types/Contato";
import { api } from "./Api";
import { AuthService } from "./AuthService";

const getHeaders = () => {
  const token = AuthService.getToken();
  return {
    "Content-Type": "application/json",
    Authorization: `Bearer ${token}`,
  };
};

export class ContatoService {
  static async listar(): Promise<Contato[]> {
    const response = await api.get<Contato[]>("/contatos", {
      headers: getHeaders(),
    });
    return response.data;
  }

  static async obterPorId(id: string): Promise<Contato> {
    try {
      const response = await api.get<Contato>(`/contatos/${id}`, {
        headers: getHeaders(),
      });
      if (!response.data) {
        throw new Error('Contato não encontrado');
      }
      return response.data;
    } catch (error) {
      console.error('Erro ao obter contato:', error);
      throw error;
    }
  }

  static async criar(contato: ContatoInput): Promise<Contato> {
    const response = await api.post<Contato>("/contatos", contato, {
      headers: getHeaders(),
    });
    return response.data;
  }

  static async atualizar(id: string, contato: ContatoInput): Promise<Contato> {
    const response = await api.put<Contato>(`/contatos/${id}`, contato, {
      headers: getHeaders(),
    });
    return response.data;
  }

  static async inativar(id: string): Promise<void> {
    const response = await api.delete(`/contatos/${id}`, {
      headers: getHeaders(),
    });
    if (response.status !== 204) {
      throw new Error('Erro ao inativar contato');
    }
  }
}
