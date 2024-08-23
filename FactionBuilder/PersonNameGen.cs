using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactionBuilder
{
    public class PersonNameGen
    {
        enum gender
        {
            male,
            female,
            andro
        }
        string[] maleElvishNames = {
    "Arphenion", "Aldarion", "Aravir", "Aravorn", "Baelorin", "Beriadan", "Belthil",
    "Bregolien", "Bithron", "Brindel", "Balanor", "Brilindar", "Belvagor", "Balfirien",
    "Brasseleg", "Berethor", "Brilthien", "Bannoril", "Belegorn", "Beldorin", "Calenhir",
    "Caranthir", "Caelumir", "Curufinwe", "Curimír", "Calmacil", "Celepharn", "Círdanor",
    "Cunduilion", "Calenardhon", "Caelindor", "Cuthalion", "Curunír", "Calenfen", "Celethir",
    "Durthil", "Dorlas", "Dirhael", "Durion", "Danithil", "Drimion", "Denethor", "Dwingelot",
    "Dorfindel", "Díriel", "Dalathir", "Danel", "Dúrothil", "Elrohir", "Edrahil", "Ecthelion",
    "Elentar", "Ereinion", "Elenion", "Eluchíl", "Engwar", "Eressëa", "Falathar", "Finrod",
    "Fëanor", "Fingolfin", "Finarfin", "Fingon", "Findecano", "Forlindon", "Faelond", "Felagund",
    "Gwindor", "Gil-galad", "Glorfindel", "Galdor", "Galathil", "Gorlim", "Galion", "Gwaeron",
    "Galdwen", "Haldir", "Haldan", "Herumor", "Huor", "Haldir", "Halmir", "Helcaraxë", "Halifirien",
    "Harnen", "Hyarmendacil", "Híril", "Hírilorn", "Isilme", "Irmo", "Imrahil", "Ithron", "Ithilwen",
    "Inglor", "Ilbereth", "Irmo", "Jandur", "Jaril", "Jelian", "Jorundil", "Jorvin", "Jarion",
    "Junith", "Javorn", "Jeleth", "Jorvin", "Kaelith", "Kandor", "Kirion", "Kithral", "Kaelen",
    "Kolvir", "Kareldil", "Kirindor", "Kaldir", "Karmir", "Kenurion", "Legolas", "Lindir",
    "Lomion", "Lanethil", "Luminor", "Lassilome", "Lúvion", "Lissuin", "Lithoniel", "Lorinath",
    "Mithrandir", "Maedhros", "Maglor", "Mablung", "Mablung", "Morifinwë", "Mithrellas",
    "Myrhil", "Morwë", "Mithlin", "Melinath", "Morion", "Mithlin", "Nerdanel", "Narcil",
    "Námo", "Ninquë", "Númenor", "Narvi", "Nindor", "Orodreth", "Ohtar", "Oropher", "Orophin",
    "Oromë", "Ornal", "Orndil", "Ondolindë", "Orleg", "Olwion", "Palantir", "Poldórion",
    "Pilin", "Pelior", "Péredhel", "Quendil", "Quenion", "Rúmil", "Rhovanor", "Rodwen", "Rog",
    "Ragnor", "Rindel", "Rilthien", "Rhovanor", "Rodwen", "Rog", "Silmaril", "Sindar", "Sauron",
    "Tauriel", "Tarannon", "Tar-Míriel", "Telemnar", "Thranduil", "Turgon", "Thalion", "Thrandir",
    "Thurandil", "Thrandor", "Túrin", "Thrandor", "Thingol", "Varda", "Voronwë", "Vardamir",
    "Valandil", "Vilya", "Vorindir", "Valacar", "Valandor", "Voronil", "Voronwe", "Voronel",
    "Voronil", "Voronildur", "Wilwarin", "Winthelas", "Wendil", "Wethar", "Wilrim", "Wynthor",
    "Wiloril", "Wilrun", "Wythil", "Wilbor", "Wyndor", "Yavandir", "Yavandir", "Ytheril",
    "Yvriel", "Zimran", "Zindor", "Zirieth", "Zamorin", "Zynan", "Zilwen", "Zaryan", "Zimrathor",
    "Zindoril", "Ziramel", "Zarnor", "Zyril", "Zirion", "Zanyar", "Zinmir", "Zarion", "Zylarin",
    "Zirionel", "Zardil", "Zithron", "Zorvan", "Zirnir", "Zilmir", "Zarith", "Zynlor", "Zirnor",
    "Zirenil", "Zarionel", "Zynoril", "Ziriel", "Zarloth", "Zythorin", "Ziranthil", "Zyrathor",
    "Zirniel", "Zarwin", "Zythil", "Zirathil", "Zorenil", "Zythril", "Zirnoth", "Zorinth",
    "Zyron", "Ziviel", "Zirielon", "Zorrin", "Zorwin", "Zyvanor", "Zirendir", "Zorwinor",
    "Zyrandor", "Zivrin", "Zorwen", "Aranel", "Aelrindel", "Alatar", "Anarion", "Beleg", "Berethor",
    "Celeborn", "Curufin", "Elrond", "Finwë", "Galadriel", "Gwindor",
    "Haldir", "Legolas", "Maeglin", "Melian", "Mithrandir", "Oropher",
    "Radagast", "Sauron", "Thranduil", "Túrin", "Voronwë"
};
        string[] femaleElvishNames = {
    "Aelrindel", "Alathrien", "Aerendyl", "Ailinora", "Andúniel", "Aeluinor",
    "Amdirien", "Anoriel", "Aegnorin", "Aelbereth", "Aranel", "Aredhel",
    "Arthien", "Aelith", "Alcarin", "Aenor", "Aelwen", "Athariel", "Amandil",
    "Almiel", "Arinthal", "Aelrond", "Andilome", "Auriel", "Anfauglir",
    "Aelkendra", "Beriothien", "Belethil", "Bithrawen", "Boronwen",
    "Brithombar", "Brinlor", "Brilwen", "Cirdaneth", "Celoril", "Cirethiel",
    "Caladwen", "Celeriel", "Carnil", "Celonwen", "Círiel", "Caelith",
    "Culurien", "Celonwen", "Calenfen", "Círiel", "Daelomin", "Deleriel",
    "Daelith", "Dorlas", "Diriel", "Dúril", "Deluindor", "Dorthalion",
    "Elenwë", "Erulissë", "Eärwen", "Erendis", "Ethuilan", "Eolande",
    "Eldalótë", "Elanor", "Estelar", "Eärendil", "Elwing", "Eledhwen",
    "Faelivrin", "Fingolfin", "Forodwaith", "Firimar", "Falmarin",
    "Findis", "Fíriel", "Faenor", "Finellach", "Faelond", "Forostar",
    "Galadhon", "Gwindeth", "Gilthoniel", "Gwathir", "Gwilwileth", "Gwindeth",
    "Galadhon", "Galenas", "Gorthaur", "Gwilioth", "Gilwen", "Gwendolen",
    "Gwindolyn", "Gwathrin", "Gwethil", "Galadhel", "Haruthalion",
    "Hareth", "Hithlum", "Hithriel", "Haradhel", "Halmë", "Harothiel",
    "Inzilbêth", "Idril", "Ilyanna", "Indis", "Inzilbêth", "Ilmare",
    "Isildë", "Isilwen", "Indilzar", "Ithilien", "Ithilwen", "Ivriniel",
    "Ivrindol", "Inziladûn", "Ilmarë", "Ilwen", "Ithilwen", "Ithiriel",
    "Indril", "Inziladûn", "Irwen", "Inzilbêth", "Irissë", "Isilmel",
    "Ivriniel", "Isilmë", "Ithilwen", "Ivrin", "Ivrindil", "Jelindra",
    "Junith", "Jalniriel", "Jeleth", "Jorindel", "Jallil", "Jaellath",
    "Jendith", "Jelia", "Kithriel", "Kaellwen", "Kaldwen", "Kithrin",
    "Kithlindë", "Kaelith", "Kolwen", "Kirindel", "Kithwen", "Kaeldil",
    "Kithwen", "Kaelwen", "Kalendil", "Lothíriel", "Laurelin", "Lúthien",
    "Lindórie", "Laurefindë", "Laegwen", "Lómëanor", "Laurewen", "Lasselanta",
    "Lassien", "Lómëanor", "Lindelë", "Lindelwen", "Melian", "Melilot",
    "Míriel", "Mithrellas", "Mithlindë", "Mellonwen", "Míreth", "Míriël",
    "Narwen", "Nellas", "Nerwen", "Nimrodel", "Nórimë", "Niniel",
    "Nimloth", "Nimrodel", "Númendil", "Nellas", "Nerwen", "Nienor",
    "Olorin", "Oropher", "Orophin", "Oromë", "Olwion", "Palantir",
    "Poldórion", "Pilin", "Pelior", "Péredhel", "Quendil", "Quenion",
    "Rúmil", "Rhovanor", "Rodwen", "Rog", "Ragnor", "Rindel", "Rilthien",
    "Rhovanor", "Rodwen", "Rog", "Silmaril", "Sindar", "Sauron", "Tauriel",
    "Tarannon", "Tar-Míriel", "Telemnar", "Thranduil", "Turgon", "Thalion",
    "Thrandir", "Thurandil", "Thrandor", "Túrin", "Thrandor", "Thingol",
    "Varda", "Voronwë", "Vardamir", "Valandil", "Vilya", "Vorindir",
    "Valacar", "Valandor", "Voronil", "Voronwe", "Voronel", "Voronil",
    "Voronildur", "Wilwarin", "Winthelas", "Wendil", "Wethar", "Wilrim",
    "Wynthor", "Wiloril", "Wilrun", "Wythil", "Wilbor", "Wyndor",
    "Yavandir", "Yavandir", "Ytheril", "Yvriel", "Zimran", "Zindor",
    "Zirieth", "Zamorin", "Zynan", "Zilwen", "Zaryan", "Zimrathor",
    "Zindoril", "Ziramel", "Zarnor", "Zyril", "Zirion", "Zanyar",
    "Zinmir", "Zarion", "Zylarin", "Zirionel", "Zardil", "Zithron",
    "Zorvan", "Zirnir", "Zilmir", "Zarith", "Zynlor", "Zirnor", "Zirenil",
    "Zarionel", "Zynoril", "Ziriel", "Zarloth", "Zythorin", "Zirniel",
    "Zarwin", "Zythil", "Zirathil", "Zorwin", "Zythril", "Zirnoth",
    "Zorinth", "Zyron", "Ziviel", "Zirielon", "Zorrin", "Zorwin", "Zyvanor",
    "Zirendir", "Zorwinor", "Zyrandor", "Zivrin", "Zorwen"
};

        static Random rnd = new();
        public static string GenerateElvenName()
        {
            return "";
        }



    }
}