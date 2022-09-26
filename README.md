# Locatudo Fail Fast Validation

Solução criada a partir do repositório [Locatudo](https://github.com/ferfujikawa/locatudo) para prática de conceitos de Fail Fast Validation, utilizando a biblioteca [Flunt](https://www.nuget.org/packages/Flunt).

**Exemplo:**

Método com aplicação do conceito de *Fail Fast Validation*:
```c#
public IRespostaComandoExecutor<DadoRespostaComandoCadastrarEquipamento> Executar(ComandoCadastrarEquipamento comando)
{
    /*
    Fail Fast Validation
    Caso o comando seja inválido, retorna notificações antes de realizar consultas ao repositório
    */
    if (!comando.Validar())
        return new RespostaGenericaComandoExecutor<DadoRespostaComandoCadastrarEquipamento>(false, null, comando.Notifications);

    var equipamento = new Equipamento(comando.Nome);
    _repositorioEquipamento.Criar(equipamento);

    return new RespostaGenericaComandoExecutor<DadoRespostaComandoCadastrarEquipamento>(
        true,
        new DadoRespostaComandoCadastrarEquipamento(equipamento.Id, equipamento.Nome),
        "Sucesso",
        "Equipamento cadastrado");
}
```

Método *Validar()* chamdo na execução do comando acima:
```c#
public bool Validar()
{
    AddNotifications(new ContratoComandoCadastrarEquipamento(this));

    return IsValid;
}
```

Construtor do contrato chamado no método *Validar()*:
```c#
public ContratoComandoCadastrarEquipamento(ComandoCadastrarEquipamento comando)
{
    Requires()
        .HasMinLength(comando.Nome, 3, "Nome", "O nome do equipamento precisa conter no mínimo 3 caracteres");
}
```
