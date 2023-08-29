using MongoDB.Bson;
using MongoDB.Driver;
using WhoKnowsKnows.Common.Entities;

namespace WhoKnowsKnows.Common.Data
{
    public class QuestionContextSeed
    {
        public static void SeedData(IMongoCollection<Question> questionCollection)
        {
            var existQuestions = questionCollection.Find(q => true).Any();
            if (!existQuestions)
            {
                questionCollection.InsertManyAsync(GetQuestionsSeed());
            }
        }

        public static IEnumerable<Question> GetQuestionsSeed()
        {
            return new List<Question> {
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 0,
                    Text = "Pravo ime supruge despota Đurađa Brankovića, u narodu poznatija kao \"prokleta Jerina\" je:",
                    Answers = new List<string> {"Milica", "Irena", "Roksanda"},
                    CorrectAnswer = "Irina"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 1,
                    Text = "Gde su se održale Zimske olimpijske igre 1984. godine?",
                    Answers = new List<string> {"Montreal", "Torino", "Salcburg"},
                    CorrectAnswer = "Sarajevo"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 2,
                    Text = "Mokasine spadaju u:",
                    Answers = new List<string> {"Odeću", "Hranu", "Alat"},
                    CorrectAnswer = "Obuću"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 3,
                    Text = "Koja od sledećih ajkula zalazi duboko uzvodno uz reke?",
                    Answers = new List<string> {"Čekićara", "Velika bela ajkula", "Tigar ajkula"},
                    CorrectAnswer = "Bik Ajkula"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 4,
                    Text = "Ko igra glavnu ulogu, pored Demi Mur, u filmu \"Duh\"?",
                    Answers = new List<string> {"Ričard Gir", "Endi Garsia", "Brus Vilis"},
                    CorrectAnswer = "Patrik Svejzi"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 5,
                    Text = "Astronom Tiho Brahe je po nacionalnosti:",
                    Answers = new List<string> {"Holanđanin", "Nemac", "Belgijanac"},
                    CorrectAnswer = "Danac"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 6,
                    Text = "Reka Nil se formira ušćem Plavog i:",
                    Answers = new List<string> {"Etiopijskog Nila", "Istočnog Nila", "Crnog Nila"},
                    CorrectAnswer = "Belog Nila"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 7,
                    Text = "Koji kontinent ima najviše stanovnika?",
                    Answers = new List<string> {"Afrika", "Evropa", "Amerika"},
                    CorrectAnswer = "Azija"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 8,
                    Text = "Koje godine je Vuk Karadžić objavio prevod knjige \"Novi zavet\"?",
                    Answers = new List<string> {"1869.", "1859.", "1850."},
                    CorrectAnswer = "1847."
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 9,
                    Text = "Ako ste u Londonu, u kom pravcu je Madrid?",
                    Answers = new List<string> {"Severno", "Istočno", "Zapadno"},
                    CorrectAnswer = "Južno"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 10,
                    Text = "Koja reka protiče kroz Loznicu",
                    Answers = new List<string> {"Miljacka", "Jadar", "Drina"},
                    CorrectAnswer = "Štira"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 11,
                    Text = "Koji od navedenih klubova je trenirao Milovan Rajevac?",
                    Answers = new List<string> {"Smederevo", "Jagodina", "OFK Beograd"},
                    CorrectAnswer = "Vojvodina"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 12,
                    Text = "Gaus je merna jedinica veća od Tesle za:",
                    Answers = new List<string> {"10 puta", "100 puta", "1000 puta"},
                    CorrectAnswer = "10000 puta"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 13,
                    Text = "Ko je napisao pripovetku \"Sve će to narod pozlatiti\"?",
                    Answers = new List<string> {"Valdislav Petković Dis", "Branislav Nušić", "Ivo Andrić"},
                    CorrectAnswer = "Laza Lazarević"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 14,
                    Text = "U kom filmu nije glumio Leonardo Dikaprio?",
                    Answers = new List<string> {"Titanik", "Uhvati me ako možeš", "Marvinova soba"},
                    CorrectAnswer = "Meksikanac"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 15,
                    Text = "Asirska reč \"ereb\" znači:",
                    Answers = new List<string> {"Izlazak sunca", "Moreuz", "Nepoznata zemlja"},
                    CorrectAnswer = "Zalazak sunca"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 16,
                    Text = "Koliko vremenskih zona ima u Rusiji?",
                    Answers = new List<string> {"12", "11", "14"},
                    CorrectAnswer = "9"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 17,
                    Text = "Automobili marke \"Mercedes\" potiču iz:",
                    Answers = new List<string> {"Italije", "Rusije", "Španije"},
                    CorrectAnswer = "Nemačke"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 18,
                    Text = "Što je za Grke bio Eros, to je za Rimljane bio:",
                    Answers = new List<string> {"Vulkan", "Jupiter", "Mars"},
                    CorrectAnswer = "Kupidon"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 19,
                    Text = "Ime kog planinskog vrha u doslovnom prevodu znači \"kameni stražar\"?",
                    Answers = new List<string> {"Himalaji", "Mon Blan", "Atlas"},
                    CorrectAnswer = "Akonkagva"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 20,
                    Text = "Koja država je u drugom svetskom ratu bila na strani sila osovine?",
                    Answers = new List<string> {"Australija", "Švajcarska", "Belgija"},
                    CorrectAnswer = "Mađarska"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 21,
                    Text = "Šta je \"humerus\"?",
                    Answers = new List<string> {"Građevinski materijal", "Starogrčki pisac", "Merni instrument"},
                    CorrectAnswer = "Ramena kost"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 22,
                    Text = "Asirska reč \"ereb\" znači?",
                    Answers = new List<string> {"Izlazak sunca", "Moreuz", "Nepoznata zemlja"},
                    CorrectAnswer = "Zalazak sunca"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 23,
                    Text = "\"U magnovenju\" je album grupe:",
                    Answers = new List<string> {"Partibrejkers", "Direktori", "Džukele"},
                    CorrectAnswer = "Goblini"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 24,
                    Text = "Kako se zvao sin Vukašina Mrnjavčevića?",
                    Answers = new List<string> {"Dušan", "Lazar", "Uglješa"},
                    CorrectAnswer = "Marko"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 25,
                    Text = "Tahikardija je:",
                    Answers = new List<string> {"Preplet creva", "Moždani udar", "Upala pluća"},
                    CorrectAnswer = "Ubrzani rad srca"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 26,
                    Text = "Koji je najveći grad Republike Srpske?",
                    Answers = new List<string> {"Prijedor", "Čajniče", "Goražde"},
                    CorrectAnswer = "Banja Luka"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 27,
                    Text = "Longitudinalni i trasferzalni su:",
                    Answers = new List<string> {"Brojevi", "Mikroprocesori", "Uglovi"},
                    CorrectAnswer = "Talasi"
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 28,
                    Text = "Koje godine je Albert Ajnštajn postavio teoriju relativiteta?",
                    Answers = new List<string> {"1911.", "1912.", "1906."},
                    CorrectAnswer = "1905."
                },
                new Question {
                    Id = ObjectId.GenerateNewId().ToString(),
                    NumId = 29,
                    Text = "Koliko cifara ima JMBG u Srbiji?",
                    Answers = new List<string> {"9", "11", "15"},
                    CorrectAnswer = "13"
                },
            };
        }
    }
}
