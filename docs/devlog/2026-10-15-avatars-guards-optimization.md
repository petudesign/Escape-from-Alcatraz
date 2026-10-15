# Pelaajakeho, vartijat, valvottu porras ja optimointi

15.10.2026. Käyttäjän palaute ohjasi tätä päivitystä: pinnat särisevät; pelaaja tarvitsee kädet ja jalat sekä muille näkyvän hahmon; vangit ovat liian samanlaisia ja paidan etumus näyttää kravattimaiselta; vartijoita puuttuu; ruokala on pieni ja porras jää valvonnalta piiloon. Lisäksi käyttäjä pyysi säilyttämään vain uusimman peliversion sekä huomioimaan koon ja suorituskyvyn jatkuvasti.

## Särinä ja kustannus

Toteutuksesta löytyi samassa tasossa olevia lattioita: pohjalaatta ja sellien omat lattiat sekä porrastilan/tasanteiden ja kerroskäytävien päällekkäisiä osia. Niiden kilpaileva syvyys voi näyttää valaistuksen särinältä. Päällekkäisyydet poistettiin. Lattian pienessä tekstuurissa ei ollut mipmappeja; ne ja suodatus lisättiin. Proseduraalisen materiaalin tiheää kohinaa suodatetaan etäisyyden mukaan ja sen hash korvattiin vakaammalla laskennalla. Kiiltoa hillittiin.

Varjojen etäisyys, bias ja valomäärä rajattiin. [Unityn varjojen vianmääritys](https://docs.unity.com/en-us/engine/6000.0/manual/lighting-overview/shadows/shadow-performance) tukee biasin ja varjoetäisyyden merkitystä, mutta käyttäjän koko havaintoa ei voi todistaa yhden still-kuvan perusteella. Liikkuvan kuvan lopullinen arvio kuuluu seuraavaan käyttäjän katselmointiin.

Yhdistetyn geometrian jälkeen turhat lähdeobjektit ja colliderit poistetaan. Pistevarjovalojen määrä vähenee. Hahmon osat on jo yhdistetty nivelittäin ja värimateriaalit jaetaan. [Pysyvä suorituskykykäytäntö](../PERFORMANCE.md) ja AGENTS.md tallentavat käyttäjän tavoitteen jatkotyölle.

## Hahmot ja pelaajan keho

Kahdeksan ulkoasudataa eroaa pituudessa, leveydessä, leuassa, kasvojen muodossa, hiuksissa, parrassa, ryhdissä ja työvaatetuksen sävyssä. Pitkä tumma paidan keskiosa poistettiin ja korvattiin pienillä napeilla. Yksikään vangin tai vartijan hahmo ei saa kravattia.

Paikallinen pelaaja ja verkkopari käyttävät samaa kokonaista hahmorakennetta. Omassa kamerassa pää ja ylävartalon renderöinti ovat vain varjossa näkyviä, jotta ne eivät peitä käsiä ja jalkoja. Muut pelaajat näkevät koko hahmon. Tämä on ensimmäinen raaja-/kehototeutus, ei valmis tartunta-animaatio, koko kehon IK tai lopullinen first-person-animaatio.

Vanha kapselihahmo korvattiin uuden ympäristön verkkokokeessa mesh-hahmolla. Sijainti, katseen suunta ja pelaajan tunnisteeseen sidottu ulkoasu näkyvät toiselle asiakkaalle. Paikallinen yhteys on edelleen prototyyppi; NPC-simulaatiota ei vielä synkronoida yhteiseksi maailmaksi, Steam-liittymistä ei ole ja liikkeen lopullinen auktoriteetti on avoin.

## Vartijat ja tilat

12 vankia ja kolme paikallaan vartioivaa vartijaa. Sinertävä univormu, lakki ja rintamerkki erottavat vartijan vangista. Vartiointipisteet ovat avoimen porrastilan edessä, kerroskäytävän päädyssä ja valvontahallissa. Vartijoilla ei vielä ole pelaajan rikkomuksiin reagoivaa havainto-/kiinniottojärjestelmää.

Porras siirtyi noin viisi metriä sisään osaston avoimeen päätyyn. Porraskoppi poistui ja näkymä avautuu osastoon. Uusi kulkukoe paljasti vielä kaiteen tukkivan poikittaisen kerroskäytävän; se lyhennettiin kulkuaukon kohdalta. Kahden vartiointipisteen näkölinjaa portaalle tarkistetaan geometriasta erillään AI-käyttäytymisestä.

Ruokala kasvoi noin 22 × 18 metriin. Pöytäryhmät ja tarjoilutiski jättävät selkeän keskikäytävän sisäänkäynniltä perälle. Uusi käyttäjän blueprint-kuva säilytetään moodi- ja tilareferenssinä, eikä sen aitoutta tai mittoja käsitellä vahvistettuina historiatietoina.

## Tarkistukset

Version 08 Mac-build, pelikuvat, pelaajakapselin kulku kaikkiin kerroksiin ja ruokalan läpi, kahden erillisen peliprosessin hahmonäkyvyys ja lyhyt renderöinnin ruutuaikakoe. Tulokset lisätään docs/evidence/update-08-kansioon. Lopulliset tulokset kirjataan työn lopuksi; testiohjelman olemassaolo ei yksin ole hyväksytty tulos.

## Lopullinen näyttö

- Build ilman C#- tai shader-käännösvirheitä; lopullinen sovelluskoko 91.91 MiB.
- Pelaajakapselin 34 kulkupistettä läpäisty, mukaan lukien molemmat ylemmät kerrokset ja laajennetun ruokalan perä.
- Molemmat tarkistetut vartiointipisteiden näkölinjat portaalle ovat avoimet. Näkölinjakokeessa läpinäkyvät turvatörmäyspaneelit eivät ole optisia esteitä.
- Host ja client läpäisivät erillisissä paikallisissa prosesseissa täyden mesh-hahmon tarkistuksen. Molemmat prosessit poistuivat koodilla 0. Lopulliset kuvat remote-host.png ja remote-client.png.
- 1440 × 900, MSAA4, lyhyt rajaamaton renderöintikoe: mediaanit osasto 5,20 ms, porras 2,20 ms, ruokala 1,70 ms, piha 0,70 ms. Raskaimman näkymän p95 5,39 ms. Unityn varaama muisti 152 MiB. 1176 renderer-komponenttia koko kohtauksessa; ei sama kuin näkyvät draw callit. Tämä ei ole erillinen GPU-profilointi tai julkaisu-FPS-lupaus.
- Vanhat seitsemän sovellusta poistettiin; noin 720,9 MiB vapautui. Builds/macOS sisältää vain PrisonCoop-08.app.

Raajojen lepokäytös ja muut animaatiot ovat edelleen prototyyppiä. Vartijoiden tekoäly, yhteinen NPC-simulaatio, Steam-liittyminen ja varsinainen palvelinauktoriteetti odottavat jatkoa. Särinän tunnetut pintapäällekkäisyydet ja suodatuspuutteet korjattiin; käyttäjän liikkuvan kuvan katselmointi voi edelleen paljastaa yksittäisiä varjoreunoja tai muita viimeistelytarpeita.
