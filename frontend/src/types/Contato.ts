export interface Contato {
  id: string;
  nome: string;
  email: string;
  telefone: string;
  dataCriacao: Date;
  dataAtualizacao?: Date;
}

export interface ContatoInput {
  id: string;
  nome: string;
  email: string;
  telefone: string;
}
