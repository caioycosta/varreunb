# varreunb
Robô para varrer a oferta do Matrícula Web - UnB

Contribuições serão bem-vindas.

O robô é composto por 3 projetos:

- *CodeMQ:* outra biblioteca de minha autoria, para troca de mensagens entre threads. Página do projeto: https://codemq.codeplex.com/
- *VarreUnb:* código principal do robô
- *MonitoramentoVagas:* utiliza o VarreUnB e executa-o periodicamente em uma turma específica, e envia um email caso o número de vagas disponíveis se altere. O método de enviar email não foi publicado aqui no github, pois continha informações pessoais.

Para executar/trabalhar no projeto, baixem a versão free do Visual Studio aqui --> https://www.visualstudio.com/pt-br/products/visual-studio-community-vs.aspx . Abrir a solution VarreUnB.sln

Para o parse do HTML é utilizada a excelente biblioteca HtmlAgilityPack - https://htmlagilitypack.codeplex.com - que não está inclusa devido à licença. Foi utilizada a versão 1.4.0.0 dessa biblioteca. Não é nada demais, só pegar a DLL e acrescentar nas referências do projeto.

O código que realiza o Parse das páginas de oferta está em RoboUnb.cs. 

Como era utilizado apenas para uso pessoal e corriqueiro a cada semestre, não há qualquer tipo de interface ou configuração. Para executar, editar o Program.cs para as suas necessidades. 

Possui dois modos de operação: varredura do site e filtragem de disciplinas ("relatório"). Para escolher qual das duas, tem um if gigante no início.

Para acelerar a varredura são utilizadas 20 threads worker para processar disciplinas + a principal que lê os departamentos e vai repassando as disciplinas para as threads. Leva uns 10 minutos para coletar toda a oferta do MW. A lista completa de todas as turmas é serializada e salva em um XML que é utilizado para buscas posteriores.

Logo abaixo há uns loops que filtram essa lista baseada em critérios como ter vagas, aulas no sábado, etc. Aqui entra a necessidade de cada um.

