# Projektin nykyinen briiffi

14.10.2026. Lähde: käyttäjän toimittama Project brief. Nämä ovat käyttäjän suunnittelulinjauksia, eivät väitteitä jo toteutetuista ominaisuuksista. Tämä dokumentti korvaa aiempien suunnitelmien ristiriitaiset tuotelinjaukset; vanhat devlogit säilyvät kehityshistoriana.

## Tuote ja pelin ydin

Kaupallinen Steam-peli: 2–4 pelaajan tyylitelty ensimmäisen persoonan co-op-vankilapako 1960-luvun amerikkalaisesta saarivankilasta. Oma maailma ja lore, vahvasti Alcatrazista ja aikakauden vankiloista inspiroitu; ei 1:1-kopio.

Fokus on vankila-arjessa, yhteisen paon valmistelussa, suhteissa vartijoihin ja vankeihin, tiedossa, palveluksissa, veloissa, vaihtokaupassa, vapaassa AI-NPC-puheessa ja co-op-improvisaatiossa. Virheet tuottavat uusia vaikeuksia ja tilanteita (soft failure), eivät jatkuvia game overeita. Survival ja item-statseilla optimointi eivät ole pelin ydin.

## Päivärytmi ja työ

Countit, työvuorot, yard time, ruokailut, tarkastukset, vapaa-aika ja lockdown muodostavat vankilan rytmin. Työ on privilege ja ennen kaikkea pääsyä tiloihin, ihmisiin ja mahdollisuuksiin. Mahdollisia work detaileja: kitchen, laundry, workshop, maintenance, cleaning, library ja hospital. Työ voi tuottaa pienen virallisen tulon henkilökohtaiselle prison accountille.

## Kolme taloutta

Ei party walletia.

| Talous | Sisältö ja sääntö |
|---|---|
| Official account | Henkilökohtainen saldo, työstä ansaittu raha ja commissary-ostot. Ei käteistä käteen. |
| Physical goods | Tupakka, kahvi, karkki, radio, lehdet ja muut hyödylliset tavarat. |
| Social economy | Palvelukset, velat, tieto, suhteet ja contraband. |

Pelaajat voivat yhdistää resursseja epäsuorasti: jokainen ostaa omalta tililtään arvotavaroita, joita vaihdetaan NPC:iden kanssa. Ryhmän yhteistyö ei edellytä yhteistä rahasaldoa.

Itemit avaavat tilanteita, eivät buffeja. Ei syö/juo jotta stamina nousee -mekaniikkaa. Radio mahdollistaa musiikin, tiedon, harhautuksen ja äänen peittämisen; korttipakka sosiaalisen tilanteen tai gamblingin; tupakka vaihtokaupan; paperi viestinnän; työkalu konkreettisen teon.

## NPC:t ja ääni

Vapaa mikrofoni-puhe korvaa dialogivalikot. NPC:llä on persoonallisuus, muisti, tieto, suhteet, nykytila ja havainnot. LLM ei muuta world statea suoraan: pelilogiikka validoi teot ja seuraukset. Tavallinen arkinen puhe kuuluu maailmaan; jokaisen kohtaamisen ei tarvitse olla hauska. Tunnelma on autenttinen, reagoiva ja välillä kireä.

Proximity voice, seinien ja ovien vaimennus sekä ympäristöäänet vaikuttavat kuulemiseen. Vartijat voivat kuulla pelaajat. Radio voi peittää tekemisen ääntä, mutta liian kovalla se voi myös herättää huomiota.

## Taidesuunta ja historiallinen käsittely

Stylized, low-poly-ish, faceted 3D. Ei realismia tai fotorealistista UE-lookkia. Yhtenäinen modulaarinen hahmojärjestelmä: eri pituudet, vartalotyypit, ihonsävyt, kasvot, hiukset, ilmeet ja posturet. Vaatteissa variaatiota yhtenäisen 1960s prison/workwear -linjan sisällä. Tavoitteena ei ole sata täysin uniikkia hahmoa.

Aikakauden epäreiluudet ja rotuerottelu voivat näkyä maailmassa. Ei suoria racial slurreja tai shokkihakuista käsittelyä. Rakenteellinen ja sosiaalinen kitka näkyy käytännöissä, ruokailussa, asumisessa, puhetyylissä ja NPC-asenteissa. Yksityiskohdat tutkitaan ennen historiallisten väitteiden tekemistä.

Nykyiseen paikalliseen hahmokokeeseen käyttäjä pyysi vaaleaihoisen selliosaston. Se ei lukitse koko pelin hahmovalikoimaa tai vahvista erottelun tarkkaa historiallista rakennetta.

## UI

Low-HUD / diegetic-ish: prison paperwork, typewriter/stencil, off-white paper ja institutionaaliset lomakkeet, vähän overlayta. Ei isoja trust/suspicion-bareja, minimap-clutteria tai sci-fi UI:ta. Tietoa opitaan ensisijaisesti maailmasta.

## Kampanja ja loppu

Noin 8–12 tunnin ensimmäinen läpipeluukokemus on tavoite, ei vielä mitattu kesto. Replayability syntyy vapaudesta, eikä ole päätavoite. Pelaajahahmot voivat olla olleet vankilassa eri ajan: eri work detailit, suhteet, rahamäärät ja historia.

Lopussa pelaajat pakenevat lautalla tai pienellä aluksella pimeyteen. Kohtalo jää epäselväksi. Viimeisessä kohtauksessa yksi paenneista lukee uutista paosta; kasvoja ei näytetä. Lehti vihjaa paenneiden oletettuun kuolemaan.

Mahdollinen jatko-osa voi vaihtaa singleplayeriin, 1–2 pelaajan co-opiin tai jatkaa co-opina. Ajatus on ”get out” → ”stay out”; jatko-osa ei ole nykyisen tuotannon vaatimus.

## Toteutustilanne

Unityssa on tutkittava visuaalinen vankila-alue, liikkuminen, esineen nosto ja herkkyysasetukset. Versio 04 lisää seitsemän reiteillä kävelevää vankia neljällä ulkoasuvariantilla. Tämä ei vielä toteuta NPC-puhetta, sosiaalista taloutta, päivärytmiä tai kampanjaa. Aiempi paikallinen verkkokoe säilyy erillään visuaalisesta versiosta.

Seuraava suunnitteluehdotus: pieni vankilan arjen tilanne, jossa työ antaa pääsyn paikkaan tai tietoon ja palvelus avaa paon valmistelua. Tämä on AI:n ehdotus jatkoksi, ei käyttäjän uusi lukittu päätös.

## Toteutuspäivitys 15.10.2026

Versio 05 tarkentaa ensimmäisen näkymän hahmoja, materiaaleja ja valaistusta sekä korjaa kohtaamisjumin paikallisella väistöllä. [Devlog](devlog/2026-10-15-polish-navigation.md) ja [modulaarisuuden rakenne](MODULARITY.md). Briiffin kampanja- ja järjestelmätavoitteet säilyvät toteuttamattomina.

## Rakennuksen tarkennus 15.10.2026

Käyttäjä vahvisti kolme sellikerrosta, suuremman pihan ja valvotun sisäisen siirtymän ennen ulosmenoa. Version 07 [blueprint](PRISON-LAYOUT.md) kuvaa toteutuksen. AI:n mitoitusehdotus on 60 selliä ja 32 × 24 m yard; määrät eivät ole käyttäjän lukitsemia kampanjavaatimuksia.

## Version 08 tarkennukset 15.10.2026

Käyttäjän pysyvät toiveet: optimointi ja kohtuullinen koko mukaan kaikkeen toteutukseen; vain uusin tarkistettu pelisovellus säilytetään. Pelaajalle näkyvät raajat, muille näkyvä hahmo, erottuvammat vangit ilman kravatteja, vankeja harvemmat pääosin paikallaan vartioivat vartijat sekä avoimesti valvottava porras ja suurempi ruokala. [Version 08 devlog](devlog/2026-10-15-avatars-guards-optimization.md) erottaa toteutuksen ja vielä puuttuvan pelilogiikan.

### 15.10.2026 — käyttäjän animaatiotarkennus
Luonnollisempi kävely ja oma keho: alas katsottaessa pitää näkyä jalkojen lisäksi vartalo ja lantio. Kädet eivät saa jäädä epäluonnolliseen vakioasentoon.

### 15.10.2026 — hahmoreferenssi
Käyttäjä vahvisti alkuperäisen hahmokuvan tyylin tavoitteeksi. Kasvot ja vaatteiden muodot lähemmäs sitä; vaatetus sovitetaan fiktiiviseen 1960-luvun vankilaan, ilman kravatteja.
