using FestiDB.Domain;
using FestiDB.Domain.Answers;
using FestiDB.Domain.Roles;
using FestiDB.Domain.Table;
using FestiMS.Manager;
using FestiMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;

namespace FestiMS.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MobileServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;


            SetSqlGenerator("System.Data.SqlClient", new EntityTableSqlGenerator());
            ContextKey = "FestiMS.Models.MobileServiceContext";
        }


        protected override void Seed(MobileServiceContext context)
        {

            context.Database.ExecuteSqlCommand(
                "alter table Events drop constraint [FK_dbo.Events_dbo.Customers_CustomerId]"
            );
            context.Database.ExecuteSqlCommand(
                "alter table Events add constraint[FK_dbo.Events_dbo.Customers_CustomerId] foreign key(CustomerId) references Customers on delete set null"
            );

            context.Database.ExecuteSqlCommand(
                "alter table Advices drop constraint [FK_dbo.Advices_dbo.Events_EventId]"
            );
            context.Database.ExecuteSqlCommand(
                "alter table Advices add constraint[FK_dbo.Advices_dbo.Events_EventId] foreign key(EventId) references Events on delete cascade"
            );

            context.Database.ExecuteSqlCommand(
                "alter table Availabilities drop constraint [FK_dbo.Availabilities_dbo.Users_InspectorId]"
            );
            context.Database.ExecuteSqlCommand(
                "alter table Availabilities add constraint[FK_dbo.Availabilities_dbo.Users_InspectorId] foreign key(InspectorId) references Users on delete cascade"
            );

            context.Database.ExecuteSqlCommand(
                "alter table Availabilities drop constraint [FK_dbo.Availabilities_dbo.Events_EventId]"
            );
            context.Database.ExecuteSqlCommand(
                "alter table Availabilities add constraint[FK_dbo.Availabilities_dbo.Events_EventId] foreign key(EventId) references Events on delete cascade"
            );

            context.Database.ExecuteSqlCommand(
                "IF EXISTS (SELECT 1 from sys.objects where name = 'FK_dbo.Questions_dbo.TableQuestionColumns_KeyColumnId')" +
                    "alter table Questions drop constraint [FK_dbo.Questions_dbo.TableQuestionColumns_KeyColumnId]"
            );
            context.Database.ExecuteSqlCommand(
                "alter table Questions add constraint[FK_dbo.Questions_dbo.TableQuestionColumns_KeyColumnId] foreign key(KeyColumnId) references TableQuestionColumns on delete cascade"
            );

            context.Database.ExecuteSqlCommand(
                "alter table TableQuestionAnswerEntries drop constraint [FK_dbo.TableQuestionAnswerEntries_dbo.AbstractTableQuestionAnswerValues_Key_Id]"
            );
            context.Database.ExecuteSqlCommand(
                "alter table TableQuestionAnswerEntries add constraint[FK_dbo.TableQuestionAnswerEntries_dbo.AbstractTableQuestionAnswerValues_Key_Id] foreign key(Key_Id) references AbstractTableQuestionAnswerValues on delete cascade"
            );

            var authManager = new AuthManager(context);
            var roleManager = new RoleManager(context);
            roleManager.Create(new AppRole("Administrator"));
            roleManager.Create(new AppRole("Inspector"));
            roleManager.Create(new AppRole("Sales"));
            roleManager.Create(new AppRole("Manager"));
            roleManager.Create(new AppRole("Operational"));


            var admin = authManager.FindByName("admin@admin.nl");

            if (admin == null)
            {

                admin = new UserAccount
                {
                    Id = Guid.NewGuid().ToString("N"),
                    UserName = "admin@admin.nl"
                };
                authManager.Create(admin, "admin1");

                authManager.AddToRole(admin.Id, "Administrator");
            }

            Inspector g;
            context.Inspectors.Add(g = new Inspector
            {
                Id = Guid.NewGuid().ToString("N"),
                Locy = 51.685473,
                Locx = 5.285021,
                Role = Role.Inspector,
                BirthDate = new DateTime(2000, 1, 1),
                Email = "admin@admin.nl",
                Gender = Gender.Man,
                FirstName = "Geordi",
                LastName = "Trapman",
                Phone = "06-37441661",
                HouseNumber = "3",
                PostalCode = "5258ET",
                UserAccount = admin
            });


            var ruben = authManager.FindByName("rgj.vanrijen@stundent.avans.nl");
            if (ruben == null)
            {

                ruben = new UserAccount
                {
                    Id = Guid.NewGuid().ToString("N"),
                    UserName = "rgj.vanrijen@stundent.avans.nl"
                };
                authManager.Create(ruben, "rubiepubie");


                authManager.AddToRole(ruben.Id, "Inspector");
            }

            Inspector r;
            context.Inspectors.Add(r = new Inspector
            {
                Id = Guid.NewGuid().ToString("N"),
                Locy = 51.8195993,
                Locx = 5.5589400999999725,
                Role = Role.Inspector,
                BirthDate = new DateTime(2000, 10, 2),
                Email = "rgj.vanrijen@stundent.avans.nl",
                Gender = Gender.Man,
                FirstName = "Ruben",
                LastName = "Rijen",
                Phone = "06-10413869",
                HouseNumber = "84",
                PostalCode = "5366CB",
                UserAccount = ruben
            });

            var stijn = authManager.FindByName("smjsmits@avans.nl");
            if (stijn == null)
            {

                stijn = new UserAccount
                {
                    Id = Guid.NewGuid().ToString("N"),
                    UserName = "smjsmits@avans.nl"
                };
                authManager.Create(stijn, "stijn");

            }

            context.FestiUsers.Add(new User
            {
                Id = Guid.NewGuid().ToString("N"),
                Role = Role.Manager,
                BirthDate = new DateTime(1998, 3, 1),
                Email = "smjsmits@avans.nl",
                Gender = Gender.Man,
                FirstName = "Stijn",
                LastName = "Smits",
                Phone = "06-37441661",
                UserAccount = stijn
            });

            List<MultipleChoiceQuestionOption> options;
            var question = new Questionnaire
            {
                Theme = "Sanitair",
                Id = Guid.NewGuid().ToString(),
                Name = "Kwaliteitsronde",
                Description = "Instructies vragenlijst. Vandaag gaan jullie een inspectie uitvoeren op Paaspop over de kwaliteit van het festival met als focus de eetgelegenheden.Naast deze Gelegenheden wil Paaspop ook meer inzicht in de drukte op verschillende onderdelen in het festival. Zorg voor een duidelijke en volledige omschrijving van wat je ziet en gebruik de juiste technieken om drukte in te schatten op verschillende locaties. Probeer de algemene vragen op het einde van de dag te beantwoorden en doe dit wat uitgebreider.",
                Questions = new List<Question>
                {
                    new DrawQuestion
                    {
                        Order = 1,
                        Id = Guid.NewGuid().ToString(),
                        Description = "Draw",
                        PictureUrl =
                            "https://geordistorage.blob.core.windows.net/images/0e354538-32fb-454b-b0ad-3810bb4fc2eb.png",
                        Answers = new List<Answer>()
                        {
                            new DrawQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                ImageUrl = "https://dierenplaatjes.us/geiten/geiten-plaatje-002.jpg"
                            },
                            new DrawQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                ImageUrl = "http://www.bertenernie.nl/wp-content/Images/Plaatjes/BertenErnie.nl-Bert-en-Ernie-Plaatjes-2.jpg"
                            }
                        }
                        
                    },
                    new MeasureQuestion
                    {
                        Id = Guid.NewGuid().ToString(),
                        Description = "Wat is de lengte bij de toiletten",
                        Unit = "Meter",
                        Order = 2,
                        Answers = new List<Answer>()
                        {
                            new MeasureQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Value = 3
                            },
                            new MeasureQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Value = 5
                            }
                        }
                    },
                    new CategorieQuestion()
                    {
                        Order = 3,
                        Id = Guid.NewGuid().ToString(),
                        Description = "Categorie"
                    },
                    new MultipleChoiceQuestion
                    {
                        Order = 4,
                        Id = Guid.NewGuid().ToString(),
                        Description = "Hoe is het overzicht over het terrein",
                        Options = options = new List<MultipleChoiceQuestionOption>
                        {
                            new MultipleChoiceQuestionOption
                            {
                                Id = Guid.NewGuid().ToString(),
                                Value = "Slecht"
                            },
                            new MultipleChoiceQuestionOption
                            {
                                Id = Guid.NewGuid().ToString(),
                                Value = "Matig"
                            },
                            new MultipleChoiceQuestionOption
                            {
                                Id = Guid.NewGuid().ToString(),
                                Value = "Goed"
                            }
                        },
                        Answers = new List<Answer>()
                        {
                            new MultipleChoiceQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                ChosenOption = options[0]
                            },
                            new MultipleChoiceQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                ChosenOption = options[0]
                            },
                            new MultipleChoiceQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                ChosenOption = options[1]
                            }
                        }
                    },
                    new OpenQuestion
                    {
                        Order = 9,
                        Id = Guid.NewGuid().ToString(),
                        Description = "Hoe valt de sfeer te beschrijven",
                        Answers = new List<Answer>()
                        {
                            new OpenQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Answer = "Rustig"
                            },
                            new OpenQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Answer = "Druk"
                            },
                            new OpenQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Answer = "Mensen staan veel in groepen, en hebben het naar hun zin."
                            },
                            new OpenQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Answer = "Druk"
                            }
                        }
                    },
                    new PictureQuestion
                    {
                        Order = 6,
                        Id = Guid.NewGuid().ToString(),
                        Description = "Picture",
                        Answers = new List<Answer>()
                        {
                            new PictureQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                ImageUrl = "https://dierenplaatjes.us/geiten/geiten-plaatje-002.jpg"
                            },
                            new PictureQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                ImageUrl = "http://www.bertenernie.nl/wp-content/Images/Plaatjes/BertenErnie.nl-Bert-en-Ernie-Plaatjes-2.jpg"
                            }
                        }
                    },
                    new ScaleQuestion
                    {
                        Order = 7,
                        Id = Guid.NewGuid().ToString(),
                        Description = "Scale",
                        Min = 0,
                        Max = 10,
                        Answers = new List<Answer>()
                        {
                            new ScaleQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Value = 0
                            },
                            new ScaleQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Value = 10
                            },
                            new ScaleQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Value = 5
                            },
                            new ScaleQuestionAnswer()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Value = 5
                            }
                        }
                    },
                    new TableQuestion
                    {
                        Order = 8,
                        Id = Guid.NewGuid().ToString(),
                        Description = "Table",
                        ValueColumn = new TableQuestionMultipleColumn()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Options = new List<MultipleChoiceQuestionOption>()
                            {
                                new MultipleChoiceQuestionOption()
                                {
                                    Id =  Guid.NewGuid().ToString(),
                                    Value = "Two"
                                },
                                new MultipleChoiceQuestionOption()
                                {
                                    Id =  Guid.NewGuid().ToString(),
                                    Value = "Too"
                                }
                            },
                            Header = "ValueHeader"
                        },
                        KeyColumn = new TableQuestionStringColumn()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Header = "KeyHeader"
                        }
                    },
                    new CategorieQuestion()
                    {
                        Order = 5,
                        Id =  Guid.NewGuid().ToString(),
                        Description = "toiletten"
                    }
                }
            };
            context.Events.Add(new Event
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Foute uur",
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                Location = "Swaggerville",
                HouseNumber = "2",
                PostalCode = "5308RH",
                GeodanAdresY = "51.685473",
                GeodanAdresX = "5.285021",
                Description = "Dit is een muziek evenement. Hier word vooral 'Foute' muziek gespeelt. Het evement verwacht rond de 20.000 bezoekers op de eerste dag. Daarom moet er goed op de veiligheid gelet worden.",
                Advices = new List<Advice>()
                {
                  new Advice()
                  {
                      Id = Guid.NewGuid().ToString(),
                      Content = "",
                      Title = "Tramkade",
                  },
                  new Advice()
                  {
                      Id = Guid.NewGuid().ToString(),
                      Content = "",
                      Title = "Kaas vragen 2",
                  }
                },
                Customer = new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "PeterPop",
                    PhoneNumber = "06-12345678",
                    PostalCode = "1111AA",
                    HouseNumber = "1",
                    KvK = "12345678",
                    Contacts = new List<Contact>()
                    {
                        new Contact()
                        {
                            Id = Guid.NewGuid().ToString(),
                            FirstName = "Pieter",
                            LastName = "Pasta",
                            Email = "pieter.pasta@basta.nl",
                            Role = "CEO",
                            Note = "",
                            PhoneNumber = "06-09876543",
                        },
                        new Contact()
                        {
                            Id = Guid.NewGuid().ToString(),
                            FirstName = "Klaas",
                            LastName = "Pasta",
                            Email = "klaas.pasta@basta.nl",
                            Role = "SEO",
                            Note = "",
                            PhoneNumber = "06-09876542",
                        }
                    }
                },
                Questionnaires = new List<Questionnaire>
                {
                    question
                    }
            });
            context.QuestionnaireInspectors.Add(new QuestionnaireInspector()
            {
                Inspector = g,
                Questionnaire = question
            });

            context.Events.Add(new Event
            {
                Id = Guid.NewGuid().ToString(),
                Name = "TableQuestionEvent",
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                Location = "RastaVille",
                HouseNumber = "2",
                PostalCode = "5308RH",
                GeodanAdresY = "51.685473",
                GeodanAdresX = "5.285021",
                Description = "Dit is een evenement met een table vraag.",
                Availabilities = new List<Availability>
                {
                    new Availability
                    {
                        Id = Guid.NewGuid().ToString(),
                        HasResponded = true,
                        Inspector = g,
                        IsAvailable = true
                    },
                    new Availability
                    {
                        Id = Guid.NewGuid().ToString(),
                        HasResponded = true,
                        Inspector = r,
                        IsAvailable = true
                    }

                },
                Questionnaires = new List<Questionnaire>
                {
                        new Questionnaire
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Tabbeletjeehhhh",
                            Theme = "Tabellijst",
                            Description = "",
                            Questions = new List<Question>
                            {
                                new TableQuestion
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Description = "TableQuestion",
                                    KeyColumn = new TableQuestionStringColumn
                                    {
                                        Id = Guid.NewGuid().ToString(),
                                        Header = "Plek"
                                    },
                                    ValueColumn = new TableQuestionTimeColumn
                                    {
                                        Id = Guid.NewGuid().ToString(),
                                        Header = "Tijd"
                                    },
                                    Answers = new List<Answer>
                                    {
                                        new TableQuestionAnswer
                                        {
                                            Id = Guid.NewGuid().ToString(),
                                            AnswerEntries = new List<TableQuestionAnswerEntry>
                                            {
                                                new TableQuestionAnswerEntry
                                                {
                                                    Id = Guid.NewGuid().ToString(),
                                                    Key = new TableQuestionAnswerStringValue
                                                    {
                                                        Id = Guid.NewGuid().ToString(),
                                                        AnswerValue = "one"
                                                    },
                                                    Value = new TableQuestionAnswerTimeValue
                                                    {
                                                        Id = Guid.NewGuid().ToString(),
                                                        AnswerValue = DateTime.Now
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
            });

            context.Customers.Add(new Customer
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Paaspop",
                PhoneNumber = "06-12345677",
                PostalCode = "1111AB",
                HouseNumber = "10",
                KvK = "12345678",
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = "Jan",
                        LastName = "Werkenaar",
                        Email = "jannie.pannie@hotmail.nl",
                        Role = "CEO",
                        Note = "",
                        PhoneNumber = "06-09876543",
                    }
                }
            });
            base.Seed(context);
        }
    }
}