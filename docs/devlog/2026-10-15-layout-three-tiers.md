# Kolme kerrosta ja vankilan tavallinen kulku

15.10.2026. Käyttäjän palautteen perusteella tehty version 07 rakennemuutos.

## Ongelma ja käyttäjän linjaus

Vanha kaksi kerrosta / 24 selliä -koe ei vastannut käyttäjän referenssien kolmea kerrosta, pitkää osastoa ja toimintojen suhteita. Selliosastolta pääsi suoraan pihalle. Piha oli pieni. Portaat jäivät liian ahtaaseen paikkaan ja välipohjan törmäys esti nousun. Aiempi visuaalinen katselmointi ei todistanut portaiden kuljettavuutta.

Käyttäjä pyysi parannuksia kokonaisuuteen ja tarjoutui toimittamaan lisätietoa. Nykyinen kuvamateriaali riittää näiden konkreettisten virheiden korjaamiseen, joten lisätietoa ei vaadittu työn aloittamiseksi.

## Ratkaisu ja perustelut

Kolme kerrosta, pidempi kahden sellirivin osasto, vaaleat kaiteet, lämpimämpi kalterimaali ja katon ylävalo seuraavat referenssin rytmiä. AI:n valitsema 10 selliä per puoli per kerros tuottaa 60 selliä; se on mittakaavakoe, ei käyttäjän lukitsema väkiluku.

Porras erotettiin omaksi väljäksi sivutilakseen. Kaksi 1,95 m leveää porrassyöksyä ja lepotasanne muodostavat yhden kerrosnousun. Välipohjat rakennetaan vain lepotasanteisiin, eivät koko porrastilan yli. Portaan askelmat ovat ohuita laattoja, jotta ylempi porras ei tuki alemman syöksyn seisomakorkeutta. Sivuilla on törmäyssuoja. Vanhan portaan kulumakappaleet poistettiin entisestä paikasta, jotta osastolle ei jää kelluvaa geometriaa.

Valvontahalli on oma tila selliosaston jälkeen. Sieltä käännytään katettuun pihareittiin ja avoimen tarkastusportin läpi noin 32 × 24 m pihalle. Ruokala erkanee hallista. Portit ovat avoimia katselmointiin; kulunvalvonta, saattaminen ja vartijoiden toiminta eivät ole toteutettuja.

Alueen rakentaminen on erotettu PrisonEnvironment.Architecture.cs-osaan; selli, kalusteet, pintakuluma ja hahmojen liike pysyvät erillisinä vastuualueina. Kokonaisuus on edelleen lähdekoodista muodostettava kohtaus, ei vielä valmis prefab-kirjasto. Oma maailma ja lore säilyvät: emme kopioi kaikkia kuvan Alcatraz-tiloja tai väitä näitä mittoja historiallisiksi.

## Tarkistus

Mac-versio 07 rakennetaan Unity 6000.6.5f1:llä. Kuvat tallennetaan ajettavasta sovelluksesta. Erillinen kulkutarkistus ajaa oikeaa pelaajan CharacterControlleria ilman törmäysten poistamista: sellistä portaisiin, kahteen ylempään kerrokseen, ylimmän kerroksen ympäri, portaita alas, valvontahallin kautta pihalle ja takaisin ruokalan sisäänkäynnille. Tulokset docs/evidence/layout-07/circulation.txt.

Koe tarkistaa yhden kapselikoon ja ennalta määritellyn reitin; se ei korvaa ihmisen kokemusta ahtaudesta, monen pelaajan kohtaamisia tai kaikkien portaiden reunojen manuaalista kokeilua. Ei uusia verkko- tai mikrofonikokeita. Koko laajennetun alueen suorituskykyä ei ole profiloitu.

## Seuraava hyödyllinen palaute

Käytävän pituus, kerrosten korkeus, porrastilan väljyys ja pihan koko oman kameran kautta. Uusia referenssejä tai tarkkoja pohjamittoja ei tässä vaiheessa vaadita. Työalueiden sijoittelu on seuraava erillinen suunnittelukysymys; koko saarta ei rakenneta kerralla.

Kulkukokeen lopputulos: kaikki 33 reittipistettä läpäistiin myös portaiden sivusuojien lisäämisen jälkeen. Kapselin törmäykset pysyivät päällä. Kuvakatselmointi paljasti lisäksi vanhan fonttimateriaalin näyttävän kylttejä seinien läpi ja takapuolelta; maailman kylteille lisättiin syvyyttä testaava, yksipuolinen tekstimateriaali. Tämä viimeinen muutos tarkistettiin ajettavan sovelluksen pelikuvilla, koska se ei muuta kulkugeometriaa.
