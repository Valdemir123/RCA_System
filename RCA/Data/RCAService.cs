using RCA.Models;
using System.Linq;

namespace RCA.Data
{
    public class RCAService
    {
        private readonly RCAContext _RCAContext;

        public RCAService(RCAContext _Context)
        {
            _RCAContext = _Context;
        }

        public void Populated()
        {
            //Company
            if (!_RCAContext.Class_Company.Any())
            {
                Class_Company _Company = new Class_Company
                {
                    Id = -1,
                    StatusId = CompanyStatus.Ativo,

                    Name = "Acesso GERAL",
                    CNPJ = "000.000.000/0000-00",
                    Site = "www.Pousada.com.br",

                    ContactName = "Leonardo Paraiso",
                    Phone1 = "(031) 97129-2686",
                    Phone2 = "",
                    Email = "reservas@ranchocoracaoaberto.com.br",

                    PostalCode = "00000-000",
                    Address = "Endereço",
                    Complement = ", Complemento",
                    City = "Cidade",
                    State = "UF",
                    Country = CompanyCountry.Brasil.ToString()
                };
                _RCAContext.Class_Company.Add(_Company);

                _Company = new Class_Company
                {
                    Id = 1,
                    StatusId = CompanyStatus.Ativo,

                    Name = "Pousada Rancho Coração Aberto",
                    CNPJ = "031.736.902/0001-14",
                    Site = "www.Pousada.com.br",

                    ContactName = "Leonardo Paraiso",
                    Phone1 = "(031) 97129-2686",
                    Phone2 = "",
                    Email = "reservas@ranchocoracaoaberto.com.br",

                    PostalCode = "37930-000",
                    Address = "Estrada Capitólio",
                    Complement = "Dique Km 5, s/n - Margem do Lago",
                    City = "Capitólio",
                    State = "MG",
                    Country = CompanyCountry.Brasil.ToString()
                };
                _RCAContext.Class_Company.Add(_Company);

                //Save
                _RCAContext.SaveChanges();
            }



            //User
            if (!_RCAContext.Class_User.Any())
            {
                Class_User _User = new Class_User
                {
                    StatusId = UserStatus.Ativo,
                    CompanyId = -1,
                    TypeAccessId = UserTypeAccess.Master,

                    Name = "Valdemir P. Silva",
                    Email="valpersil@gmail.com",
                    Phone="(11) 98278-3044",

                    UserName="Valdemir",
                    Password="Inicial"
                };
                _RCAContext.Class_User.Add(_User);

                //Save
                _RCAContext.SaveChanges();
            }
            
            
            
            //Channel
            if (!_RCAContext.Class_Channel.Any())
            {
                //BALCAO
                var _Level = new Class_Channel
                {
                    StatusId = ChannelStatus.Ativo,
                    CompanyId = 1,
                    TypeId = ChannelType.BALCAO,

                    Name = "BALCÃO",
                    Tax =0,
                    Percent=0
                };
                _RCAContext.Class_Channel.Add(_Level);

                //SITE
                _Level = new Class_Channel
                {
                    StatusId = ChannelStatus.Ativo,
                    CompanyId = 1,
                    TypeId = ChannelType.SITE,

                    Name = "SITE PRÓPRIO",
                    Tax = 0,
                    Percent = 0
                };
                _RCAContext.Class_Channel.Add(_Level);

                //RESERVA
                _Level = new Class_Channel
                {
                    StatusId = ChannelStatus.Ativo,
                    CompanyId = 1,
                    TypeId = ChannelType.RESERVA,

                    Name = "Booking.com",
                    Tax = 0,
                    Percent = 3
                };
                _RCAContext.Class_Channel.Add(_Level);

                //RESERVA
                _Level = new Class_Channel
                {
                    StatusId = ChannelStatus.Ativo,
                    CompanyId = 1,
                    TypeId = ChannelType.RESERVA,

                    Name = "Expedia.com",
                    Tax = 0,
                    Percent = 4
                };
                _RCAContext.Class_Channel.Add(_Level);

                //RESERVA
                _Level = new Class_Channel
                {
                    StatusId = ChannelStatus.Ativo,
                    CompanyId = 1,
                    TypeId = ChannelType.RESERVA,

                    Name = "Trivago.com",
                    Tax = 2.5,
                    Percent = 0
                };
                _RCAContext.Class_Channel.Add(_Level);

                //Save
                _RCAContext.SaveChanges();
            }



            //GroupLevel
            if (!_RCAContext.Class_GroupLevel.Any())
            {
                //HOSPEDAGEM
                var _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupType.ACOMODACAO,

                    Name = "Acomodação MASTER"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupType.ACOMODACAO,

                    Name = "Acomodação PADRÃO"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);


                //ENTRETENIMENTO
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupType.ENTRETENIMENTO,

                    Name = "Passeio JEEP"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupType.ENTRETENIMENTO,

                    Name = "Passeio LANCHA"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupType.ENTRETENIMENTO,

                    Name = "SPA"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupType.ENTRETENIMENTO,

                    Name = "ACESSÓRIO"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);


                //CONSUMO
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupType.CONSUMO,

                    Name = "BEBIDA"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupType.CONSUMO,

                    Name = "PORÇÃO"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupType.CONSUMO,

                    Name = "REFEIÇÃO"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupType.CONSUMO,

                    Name = "LOJINHA"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);

                //Save
                _RCAContext.SaveChanges();
            }

            //GroupLevel
            if (!_RCAContext.Class_GroupLevelItem.Any())
            {
                //HOSPEDAGEM - MASTER
                var _GroupLevelId = _RCAContext.Class_GroupLevel.FirstOrDefault(m => m.Name == "Comodação MASTER");
                //
                var _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Sapequinha",
                    OccupantsNum=2,
                    PCD= GroupLevelItem_YN.Sim
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);

                //HOSPEDAGEM - PADRÃO
                _GroupLevelId = _RCAContext.Class_GroupLevel.FirstOrDefault(m => m.Name == "Comodação PADRÃO");
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Os DEZ Mandamentos do Amor",
                    OccupantsNum = 3,
                    PCD = GroupLevelItem_YN.Nao
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Enamorados",
                    OccupantsNum = 3,
                    PCD = GroupLevelItem_YN.Nao
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Me Apaxonei",
                    OccupantsNum = 3,
                    PCD = GroupLevelItem_YN.Nao
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Anjo Protetor",
                    OccupantsNum = 3,
                    PCD = GroupLevelItem_YN.Sim
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "A Carta",
                    OccupantsNum = 4,
                    PCD = GroupLevelItem_YN.Nao
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Eu Amo Você",
                    OccupantsNum = 4,
                    PCD = GroupLevelItem_YN.Nao
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                

                //ENTRETENIMENTO - JEEP
                _GroupLevelId = _RCAContext.Class_GroupLevel.FirstOrDefault(m => m.Name == "Passeio JEEP");
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "JEEP 4X4 - EXCLUSIVO",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "JEEP 4X4 - 2 pessoas",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "JEEP 4X4 - 4 pessoas",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);


                //ENTRETENIMENTO - LANCHA
                _GroupLevelId = _RCAContext.Class_GroupLevel.FirstOrDefault(m => m.Name == "Passeio LANCHA");
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "LANCHA 19p - EXCLUSIVO",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "LANCHA 19p - 2 pessoas",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "LANCHA 19p - 4 pessoas",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "LANCHA 19p - 8 pessoas",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);


                //ENTRETENIMENTO - SPA
                _GroupLevelId = _RCAContext.Class_GroupLevel.FirstOrDefault(m => m.Name == "SPA");
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Massagem INDIVIDUAL",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Sauna INDIVIDUAL",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Yoga INDIVIDUAL",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);


                //ENTRETENIMENTO - ACESSÓRIO
                _GroupLevelId = _RCAContext.Class_GroupLevel.FirstOrDefault(m => m.Name == "ACESSÓRIO");
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Proteção a prova dágua p/celular",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Mascara de mergulho",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Boné masculino",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Chapéu feminimo",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);


                //CONSUMO - BEBIDA
                _GroupLevelId = _RCAContext.Class_GroupLevel.FirstOrDefault(m => m.Name == "BEBIDA");
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Água copo 300ml",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Água garrafa 500ml",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Refrigerante lata 300ml",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Suco natural jarra 500ml",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);


                //CONSUMO - PORCAO
                _GroupLevelId = _RCAContext.Class_GroupLevel.FirstOrDefault(m => m.Name == "PORÇÃO");
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Porção de batata frita",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Porção de linguiça",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);


                //CONSUMO - REFEICAO
                _GroupLevelId = _RCAContext.Class_GroupLevel.FirstOrDefault(m => m.Name == "REFEIÇÃO");
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Prato do dia",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Prato Macarronada",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);


                //CONSUMO - LOJINHA
                _GroupLevelId = _RCAContext.Class_GroupLevel.FirstOrDefault(m => m.Name == "LOJINHA");
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Camiseta (P/M/G)",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);
                //
                _Level = new Class_GroupLevelItem
                {
                    StatusId = GroupLevelItemStatus.Ativo,
                    GroupLevelId = _GroupLevelId.Id,

                    Name = "Camiseta (PLUS SIZE)",
                };
                _RCAContext.Class_GroupLevelItem.Add(_Level);


                //Save
                _RCAContext.SaveChanges();
            }
        }
    }
}
