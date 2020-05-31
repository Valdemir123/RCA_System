using RCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCA.Data
{
    public class RCAService
    {
        private RCAContext _RCAContext;

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


            //GroupLevel
            if (!_RCAContext.Class_GroupLevel.Any())
            {
                //HOSPEDAGEM
                var _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupItem.HOSPEDAGEM,

                    Name = "MASTER"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupItem.HOSPEDAGEM,

                    Name = "PADRÃO"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);


                //CANAL
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupItem.CANAL,

                    Name = "BALCÃO"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupItem.CANAL,

                    Name = "SITE PRÓPRIO"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupItem.CANAL,

                    Name = "SITE DE VIAGEM"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);


                //ENTRETENIMENTO
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupItem.ENTRETENIMENTO,

                    Name = "PASSEIO JEEP"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupItem.ENTRETENIMENTO,

                    Name = "PASSEIO LANCHA"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupItem.ENTRETENIMENTO,

                    Name = "SPA"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupItem.ENTRETENIMENTO,

                    Name = "ACESSÓRIO"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);


                //CONSUMO
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupItem.CONSUMO,

                    Name = "BEBIDA"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupItem.CONSUMO,

                    Name = "PORÇÃO"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);
                //
                _Level = new Class_GroupLevel
                {
                    StatusId = GroupLevelStatus.Ativo,
                    CompanyId = 1,
                    GroupId = GroupItem.CONSUMO,

                    Name = "REFEIÇÃO"
                };
                _RCAContext.Class_GroupLevel.Add(_Level);


                //Save
                _RCAContext.SaveChanges();
            }

        }
    }
}
