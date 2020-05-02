namespace InterventionWebSite.Migrations
{
    using FizzWare.NBuilder;
    using InterventionWebSite.Models;
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<InterventionWebSite.Models.DataContext>
    {
        public DataContext db = new DataContext();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "InterventionWebSite.Models.DataContext";
        }

        protected override void Seed(InterventionWebSite.Models.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            Role role1 = new Role() { RoleName = "Admin", Description = "Administrateur" };
            Role role2 = new Role() { RoleName = "Reception employee", Description = "Employe de reception" };
            Role role3 = new Role() { RoleName = "Intervener", Description = "Intervenant" };
            Role role4 = new Role() { RoleName = "Assignment manager", Description = "Responsable d'affectation" };

            IList<Role> lesroles = new List<Role>();
            lesroles.Add(role1);
            lesroles.Add(role2);
            lesroles.Add(role3);
            lesroles.Add(role4);
            foreach (Role role in lesroles)
                context.roles.Add(role);
            base.Seed(context);


            var users = Builder<User>
            .CreateListOfSize(25)
            .All()
            .With(u => u.Firstname = Faker.Name.First())
            .With(u => u.Lastname = Faker.Name.Last())
            .With(u => u.Email = Faker.Internet.Email())
            .With(u => u.Phone = Faker.Phone.Number())
            .With(u => u.Password = "123456789")
            .With(u => u.Role = Pick<Role>.RandomItemFrom(lesroles))
            .Build();
            context.users.AddOrUpdate(u => u.UserId, users.ToArray());
            base.Seed(context);

            var adminlist = users.ToList().FindAll(a => a.Role.RoleName.Equals("Admin")); //just reception employees
            var assignmentlist = users.ToList().FindAll(a => a.Role.RoleName.Equals("Assignment manager")); //just reception employees
            var employeelist = users.ToList().FindAll(a => a.Role.RoleName.Equals("Reception employee")); //just reception employees
            var intervenerlist = users.ToList().FindAll(a => a.Role.RoleName.Equals("Intervener")); //just reception employees


            var agencies = Builder<Agency>.CreateListOfSize(15)
            .All()
            .With(a => a.AgencyName = Faker.Company.Suffix())
            .With(a => a.Address = Faker.Address.StreetAddress() +", "+ Faker.Address.City())
            .Build();

            context.agencies.AddOrUpdate(a => a.AgencyName, agencies.ToArray());
            base.Seed(context);

            

            Direction direction1 = new Direction() { DirectionName = "DCF", Description = Faker.Lorem.Sentence() };
            Direction direction2 = new Direction() { DirectionName = "COM", Description = Faker.Lorem.Sentence() };
            Direction direction3 = new Direction() { DirectionName = "DEA", Description = Faker.Lorem.Sentence() };
            Direction direction4 = new Direction() { DirectionName = "DSI", Description = Faker.Lorem.Sentence() };
            Direction direction5 = new Direction() { DirectionName = "BOC", Description = Faker.Lorem.Sentence() };

            IList<Direction> directionList = new List<Direction>();
            directionList.Add(direction1);
            directionList.Add(direction2);
            directionList.Add(direction3);
            directionList.Add(direction4);
            directionList.Add(direction5);
            foreach (Direction direction in directionList)
                context.directions.Add(direction);
            base.Seed(context);

            State state1 = new State() { StateName = "A affecter", Description = "En attente d'affectation" };
            State state2 = new State() { StateName = "En cours", Description = "En cours de traitement" };
            State state3 = new State() { StateName = "Cloturee", Description = "Fermee" };

            IList<State> toAssignState = new List<State>();
            toAssignState.Add(state1);
            foreach (State state in toAssignState)
                context.states.Add(state);
            base.Seed(context);

            IList<State> inProgressState = new List<State>();
            inProgressState.Add(state2);
            foreach (State state in inProgressState)
                context.states.Add(state);
            base.Seed(context);

            IList<State> closignState = new List<State>();
            closignState.Add(state3);
            foreach (State state in closignState)
                context.states.Add(state);
            base.Seed(context);



            var staffs = Builder<Staff>
            .CreateListOfSize(20)
            .All()
            .With(st => st.FirstName = Faker.Name.First())
            .With(st => st.LastName = Faker.Name.Last())
            .With(st => st.Email = Faker.Internet.Email())
            .With(st => st.Phone = Faker.Phone.Number())
            .With(st => st.Agency = Pick<Agency>.RandomItemFrom(agencies))
            .With(st => st.Direction = Pick<Direction>.RandomItemFrom(directionList))
            .Build();
            context.staffs.AddOrUpdate(st => st.StaffId, staffs.ToArray());
            base.Seed(context);

            List<string> types = new List<string>(new string[] { "Email", "Téléphone", "Directe" });

            var requests = Builder<Request>
            .CreateListOfSize(8)
            .All()
            .With(re => re.TypeOfCommunication = Pick<String>.RandomItemFrom(types))
            .With(re => re.problems = Faker.Lorem.Sentence())
            .With(re => re.RequestDate = DateTime.Now.AddDays(-2))
            .With(re => re.Staff = Pick<Staff>.RandomItemFrom(staffs))
            .With(re => re.ReceptionEmployee = Pick<User>.RandomItemFrom(employeelist))
            .Build();
            context.requests.AddOrUpdate(re => re.RequestId, requests.ToArray());
            base.Seed(context);

            var requests2 = Builder<Request>
            .CreateListOfSize(8)
            .All()
            .With(re => re.TypeOfCommunication = Pick<String>.RandomItemFrom(types))
            .With(re => re.problems = Faker.Lorem.Sentence())
            .With(re => re.RequestDate = DateTime.Now.AddMonths(-2))
            .With(re => re.Staff = Pick<Staff>.RandomItemFrom(staffs))
            .With(re => re.ReceptionEmployee = Pick<User>.RandomItemFrom(employeelist))
            .Build();
            context.requests.AddOrUpdate(re => re.RequestId, requests2.ToArray());
            base.Seed(context);

            var requests3 = Builder<Request>
            .CreateListOfSize(8)
            .All()
            .With(re => re.TypeOfCommunication = Pick<String>.RandomItemFrom(types))
            .With(re => re.problems = Faker.Lorem.Sentence())
            .With(re => re.RequestDate = DateTime.Now.AddMonths(-3))
            .With(re => re.Staff = Pick<Staff>.RandomItemFrom(staffs))
            .With(re => re.ReceptionEmployee = Pick<User>.RandomItemFrom(employeelist))
            .Build();
            context.requests.AddOrUpdate(re => re.RequestId, requests3.ToArray());
            base.Seed(context);


            List<string> natures = new List<string>(new string[] { "Drapeaux", "Eclairages", "Fenêtre", "Peintures", "Portes", "Autres" });


            var interventions = Builder<Intervention>  // Interventions cloturees
            .CreateListOfSize(10)
            .All()
            .With(i => i.ActionDate = DateTime.Now.AddDays(-2))
            .With(i => i.ClosingDate = DateTime.Now.AddDays(-1))
            .With(i => i.Nature = Pick<string>.RandomItemFrom(natures))
            .With(i => i.Description = Faker.Lorem.Sentence())
            .With(i => i.State = Pick<State>.RandomItemFrom(closignState))
            .With(i => i.Agency = Pick<Agency>.RandomItemFrom(agencies))
            .With(i => i.Direction = Pick<Direction>.RandomItemFrom(directionList))
            .With(i => i.Request = Pick<Request>.RandomItemFrom(requests))
            .With(i => i.AssignmentManager = Pick<User>.RandomItemFrom(assignmentlist))
            .With(i => i.Intervener = Pick<User>.RandomItemFrom(intervenerlist))
            .Build();
            context.interventions.AddOrUpdate(i => i.InterventionId, interventions.ToArray());
            base.Seed(context);

            var interventions2 = Builder<Intervention>  // Interventions cloturees
           .CreateListOfSize(10)
           .All()
           .With(i => i.ActionDate = DateTime.Now.AddMonths(-3))
           .With(i => i.ClosingDate = DateTime.Now.AddMonths(-2))
           .With(i => i.Nature = Pick<string>.RandomItemFrom(natures))
           .With(i => i.Description = Faker.Lorem.Sentence())
           .With(i => i.State = Pick<State>.RandomItemFrom(closignState))
           .With(i => i.Agency = Pick<Agency>.RandomItemFrom(agencies))
           .With(i => i.Direction = Pick<Direction>.RandomItemFrom(directionList))
           .With(i => i.Request = Pick<Request>.RandomItemFrom(requests))
           .With(i => i.AssignmentManager = Pick<User>.RandomItemFrom(assignmentlist))
           .With(i => i.Intervener = Pick<User>.RandomItemFrom(intervenerlist))
           .Build();
            context.interventions.AddOrUpdate(i => i.InterventionId, interventions2.ToArray());
            base.Seed(context);

            var interventions3 = Builder<Intervention>  // Interventions cloturees
           .CreateListOfSize(10)
           .All()
           .With(i => i.ActionDate = DateTime.Now.AddMonths(-2))
           .With(i => i.ClosingDate = DateTime.Now.AddMonths(-1))
           .With(i => i.Nature = Pick<string>.RandomItemFrom(natures))
           .With(i => i.Description = Faker.Lorem.Sentence())
           .With(i => i.State = Pick<State>.RandomItemFrom(closignState))
           .With(i => i.Agency = Pick<Agency>.RandomItemFrom(agencies))
           .With(i => i.Direction = Pick<Direction>.RandomItemFrom(directionList))
           .With(i => i.Request = Pick<Request>.RandomItemFrom(requests))
           .With(i => i.AssignmentManager = Pick<User>.RandomItemFrom(assignmentlist))
           .With(i => i.Intervener = Pick<User>.RandomItemFrom(intervenerlist))
           .Build();
            context.interventions.AddOrUpdate(i => i.InterventionId, interventions3.ToArray());
            base.Seed(context);

            var interventionsAAffecter = Builder<Intervention>   // Interventions non-cloturees et pas encore affectees
            .CreateListOfSize(15)
            .All()
            .With(i => i.ActionDate = null)
            .With(i => i.ClosingDate = null)
            .With(i => i.Nature =Pick<string>.RandomItemFrom(natures))
            .With(i => i.Description = Faker.Lorem.Sentence())
            .With(i => i.State = Pick<State>.RandomItemFrom(toAssignState))
            .With(i => i.Agency = Pick<Agency>.RandomItemFrom(agencies))
            .With(i => i.Direction = Pick<Direction>.RandomItemFrom(directionList))
            .With(i => i.Request = Pick<Request>.RandomItemFrom(requests))
            .With(i => i.AssignmentManagerId = null)
            .With(i => i.IntervenerId = null)
            .Build();
            context.interventions.AddOrUpdate(i => i.InterventionId, interventionsAAffecter.ToArray());
            base.Seed(context);

            var interventionsEnCours = Builder<Intervention>   // Interventions non-cloturees ms en cours de traitement

            .CreateListOfSize(15)
            .All()
            .With(i => i.ActionDate = DateTime.Now)
            .With(i => i.ClosingDate = null)
            .With(i => i.Nature = Pick<string>.RandomItemFrom(natures))
            .With(i => i.Description = Faker.Lorem.Sentence())
            .With(i => i.State = Pick<State>.RandomItemFrom(inProgressState))
            .With(i => i.Agency = Pick<Agency>.RandomItemFrom(agencies))
            .With(i => i.Direction = Pick<Direction>.RandomItemFrom(directionList))
            .With(i => i.Request = Pick<Request>.RandomItemFrom(requests))
            .With(i => i.AssignmentManager = Pick<User>.RandomItemFrom(assignmentlist))
            .With(i => i.Intervener = Pick<User>.RandomItemFrom(intervenerlist))
            .Build();
            context.interventions.AddOrUpdate(i => i.InterventionId, interventionsEnCours.ToArray());
            base.Seed(context);

    

        }
    }

}

