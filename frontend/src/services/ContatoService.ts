// src/services/ContatoService.ts
import type { Contato, ContatoInput } from "../types/Contato";
import { api } from "./Api";

export class ContatoService {
  static async listar(): Promise<Contato[]> {
    const response = await api.get<Contato[]>("/contatos");
    return response.data;
  }

  static async obterPorId(id: string): Promise<Contato> {
    const response = await api.get<Contato>(`/contatos/${id}`);
    return response.data;
  }

  static async criar(contato: ContatoInput): Promise<Contato> {
    const response = await api.post<Contato>("/contatos", contato);
    return response.data;
  }

  static async atualizar(id: string, contato: ContatoInput): Promise<Contato> {
    const response = await api.put<Contato>(`/contatos/${id}`, contato);
    return response.data;
  }

  static async inativar(id: string): Promise<void> {
    await api.delete(`/contatos/${id}`);
  }
}
