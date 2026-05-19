<h1>Api RESTful</h1>
<p>
Api simples contendo duas entidades; produtos e categorias dos mesmos. 
<h3>Diferenciais:</h3>
> A web api foi elaborada e desenvolvida seguindo as boas práticas de desenvolvimento.
  <br>
<br> > Para evitar repetição de código, foi decido por usar o padrão Repository Genérico(Generic Repository Pattern) de projeto. O padrão repository fez a mediação entre o domínio e as camadas de mapeamento de dados, agindo como uma coleção de objetos de domínio em memória. 
Portanto, há uma separação da lógica de acesso à dados. 
  <br>
<br> > Para uma interação exclusiva com o context do banco de dados(DbContext) e para modificar e preparar mudanças de persistência, foi usado uma unidade de trabalho(Unit Of Work). Portanto, apenas a mesma é responsável por chamar o SaveChange. 
  Os repositórios, por sua vez, realizam as operações de leitura e escrita no banco(MySql), mas não efetivam o SaveChange.
  <br>
  <br> > Para restringir o acesso e a exposição das informações das entidades de domínio, foi implementado o DTO(Data Transfer Objects), com isso as propriedades específicas do modelo de entidades serão OCULTADAS por questão de segurança. 
  Também há uma redução no tamanho da carga para melhorar o desempenho. O mapeamento entre os objetos que representam as entidades e os objetos que representam os DTOs foi feito automaticamente usando o AutoMapper.<br>


</p>
