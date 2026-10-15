# Taidesuunta v0.2

14.10.2026. Käyttäjän toivoma suunta on tyylitelty ja epärealistinen. Alla olevat yksityiskohdat ovat kuvan tulkintaa ja kokeiltavia ehdotuksia.

![Käyttäjän hahmotyylin referenssi](references/character-style-01.png)

Referenssi on kopioitu projektin sisälle, jotta väliaikaisen liitteen poistuminen ei kadota suunnittelun lähtökohtaa. Alkuperäinen tekijä ja käyttölupa eivät ole tiedossa. Käyttö tässä: sisäinen tyylireferenssi; kuvan julkaisemista tai hahmon kopiointia ei ole päätetty. Julkiseen devblogiin voidaan myöhemmin käyttää oman hahmon kuvaa.

## Kuvasta tunnistettavat piirteet

Kulmikkaat pinnat, suuret korvat ja kädet, pitkät raajat, vahvat kulmakarvat ja yksinkertainen hiussiluetti. Paita, solmio ja housut erottuvat suurina värialueina. Värit ovat murrettuja; iho on ympäristöä lämpimämpi. Pienet pintamuutokset antavat kulunutta tuntua ilman fotorealistisia materiaaleja.

Kuva ei yksin kerro polygonimäärää, teksturointitapaa, rigiä tai animaatiotekniikkaa. Pikselimäinen vaikutelma voi tulla tekstuurista, geometriasta tai niiden yhdistelmästä. Näitä ei lukita pelkän kuvan perusteella.

## Pelille ehdotettu tulkinta

Hahmot ovat hieman karikatyyrimäisiä aikuisia. Vaate, ryhti ja siluetti erottavat vartijan vangista; väri ei ole ainoa tunniste. Vankilan isot muodot ovat selkeitä, kuluneita ja hieman epäsäännöllisiä. Sellin esineet eivät kilpaile huomion kannalta vartijan kasvojen tai peitettävän jäljen kanssa.

Valaistuksen kokeiltava suunta: viileä käytävä, lämmin sellin valo, vahva hahmon ja taustan erottuminen. Tumma tunnelma ei saa peittää vuorovaikutuskohteita. Taidesuunta toimii sekä Unityssa että Unrealissa; realistinen renderöinti ei ole moottorivalinnan vaatimus.

**Keskeinen design-kysymys:** pystyykö pelaaja lukemaan vartijan huomion ilman mittaria? Katse, pään kääntö, tauko, askel ja puhe ovat tärkeämpiä kuin yksityiskohtainen iho. Ensimmäisen persoonan pelissä myös pelaajan kädet, paperi ja työväline ovat usein näkyvissä: koko budjettia ei suunnata kokovartalohahmoon.

## Ensimmäinen ulkoasukoe

Yksi oma vartija tai vanki, yksi käsi/esine ja sellin kulma. Vertaa samaa hahmoa keskusteluetäisyydeltä, käytävän toisesta päästä ja sivuvalossa. Kokeile kolmea havaittavaa tilannetta: huomio muualla, kuuntelee pelaajaa, tarkastaa jälkeä. Arvioi omaperäisyys, luettavuus ja animaation riittävyys. Pidä kuvat samoista kuvakulmista vertailua varten.

Mahdollinen tuotantopolku on yhteinen humanoidirigi ja vaate-/hiusvariantit. Se on tehokkuushypoteesi: testataan, säilyvätkö persoonallisuus ja tarvittavat eleet. Lopullista mallimäärää tai tekstuuribudjettia ei päätetä vielä. AI voi auttaa omaisuuden tuotannossa, mutta animaatio, mittakaava ja pelimoottoriin tuonti pitää tarkistaa ajettavassa buildissa.

## Ympäristön suunta

### Koko vankilan pääreferenssi — käyttäjän tarkennus 14.10.2026

![Sellirakennuksen yleisilme](references/environments/prison-cellblock-style-01.png)

Tämä kuva ohjaa koko vankilan ympäristötyyliä: pitkät toistuvat sellirivit, vaaleat kivimäiset/pinnoitetut rakenteet, haalistunut vihreä alaseinä, mattapintainen lattia, kalterit ja ylempien tasojen kapeat käytävät kaiteineen. Isot muodot säilytetään, pintayksityiskohdat tyylitellään hahmoreferenssin suuntaan. Realistinen valokuva on arkkitehtuurin ja värien lähde, ei tavoite fotorealistiselle grafiikalle.

Ylempien tasojen rytmi voi näkyä myös taustassa ennen kuin ne ovat pelattavia. Kerrosten määrä ja pohjapiirros ratkaistaan pelattavuudella; kuva ei tee kaikkien kerrosten rakentamisesta ensimmäisen version vaatimusta. Vaalean ympäristön on edelleen annettava hahmolle, varjolle ja pienelle peitettävälle jäljelle riittävä erottuvuus.

![Henkilökohtainen sellitila](references/environments/prison-personal-cell-01.png)

Toinen kuva täydentää sellin esinemaailmaa: kapea vuode, hyllyt, kirjat ja paperit, kuppi tai astia, riippuva vaate ja muutama henkilökohtainen esine. Soitin voi olla yhden hahmon tunniste, ei oletus jokaiseen selliin. Kuvassa näkyvä pää vuoteessa voi tukea myöhempää peite-/harhautusideaa, mutta sen tulkintaa tai mekaniikkaa ei ole vahvistettu.

Käyttäjän tarkoitus on kuvata selliin mahdollisesti sopivaa 1960-luvun tavaraa. Kuvan näyttelyjärjestelyä ja jokaista esinettä ei vielä ole todennettu historialliseksi käytännöksi. Lisätään esineitä harkiten: niiden tulee kertoa asukkaasta tai palvella pelitilannetta, eivätkä ne saa tukkia kulkutilaa tai työkalun käyttöä.

Käyttäjän vahvistama tavoite: 1960-luvun vankilan sisätilat, yksinkertaistetut ja tyylitellyt. Sellin tulee tuntua **karulta, ahtaalta ja niukasti kalustetulta**, mutta siellä pitää mahtua kävelemään. WC:n takana on pakoreitin lähtökohta. [Sellireferenssi ja kommentit](references/environments/README.md).

Tavoitteet: helposti luettavat tilat, vähän irtotavaraa, uskottava vankilatunnelma, toistettavat rakennuspalat ja kohtuullinen sisätilojen tuotantomäärä. Tämä tukee yksintekijän laajuutta ja sopii tyyliteltyihin hahmoihin.

Säilytetään kalterien rytmi, kapea vuode, pienet hyllyt, paljas WC sekä vaalean yläseinän ja haalistuneen vihreän alaseinän kontrasti. Yksinkertaistetaan ruoste, halkeamat, petivaatteiden laskokset ja posliinin muodot muutamaksi selkeäksi muodoksi ja värialueeksi. Vasta pelissä näkyvä tarve perustelee lisäyksityiskohdan.

Ahdauden tunne tehdään lähellä olevilla seinillä, kaltereilla ja kalusteiden mittakaavalla. Kameran tai hahmon jatkuva takertuminen ei ole tavoiteltu tunne. Sellin ensimmäinen ehdotettu moduuli on 2,4 × 3,6 m, korkeus 2,8 m; se on pelillinen kokeilumitta, ei Alcatrazin mitattu sellikoko. [Tilasuunnitelma](PRISON-LAYOUT.md) kertoo reitit ja liikkumiskokeet.

Myöhempi arki, esineet ja ääni: [käyttäjän toimittaman do/don’t-briefin suunnittelumuistio](ENVIRONMENT-GUIDELINES.md). Valmiit esineet ja muut tuotantopalikat arvioidaan [assettistrategian](ASSET-STRATEGY.md) mukaan. Nämä eivät laajenna ensimmäisen tilakokeen tehtävälistaa.

## Pelin kokonaisilmeen konsepti

![Käyttäjän toimittama generoitu pelikonsepti](references/game-look/prison-game-concept-01.png)

Käyttäjä toimitti tämän ChatGPT:llä generoidun kuvan 14.10.2026. Se yhdistää hahmotyylin, vankilaympäristön ja ensimmäisen persoonan kameran. Kuva on tavoitesuunnan referenssi, ei ruutukaappaus toimivasta pelistä tai todiste suorituskyvystä.

Säilytettävä yhdistelmä: kulmikas ilmeikäs hahmo, kuluneet vaaleat/vihreät pinnat, lämmin sellivalo ja viileämpi käytävä, niukka mutta asutun näköinen tila. Tyylitellyt kädet ja esineet sopivat samaan maailmaan. Pelattavuuskokeessa katseen, vartijan huomion ja piilotettavan jäljen pitää edelleen erottua.

Kuvan pakosuunnitelma, pikavalintapaikat, nimi Rico, E Talk -toiminto, radio ja mikrofonimittari ovat esitysideoita. Niitä ei hyväksytä ominaisuuksiksi kuvan tekstien perusteella. Erityisesti puheen käyttöliittymä suunnitellaan aiemman vapaan NPC-puheen tavoitteen mukaan. Syväterävyys tai tumma kuvankäsittely ei saa tehdä liikkumisesta tai havaintojen lukemisesta vaikeaa.

### Miten tavoite sovitetaan suorituskykyyn?

Suunnitteluarvio: tämän kaltainen tyylitelty ilme on varteenotettava tavoite. Kuvan yksityiskohtien määrää, animaation laatua tai ruudunpäivitystä ei voi päätellä yhdestä generoidusta kuvasta. Omat mallit, materiaalit, valo ja kuva-ala on testattava ajettavassa pelissä.

Kokeiltava toteutussuunta: toistettavat pintamateriaalit, kulumat pääosin tekstuurissa, harkittu esinemäärä ja paikallaan pysyvien valojen leivonta. Unity kuvaa lightmappingin vaihtokaupaksi, jossa valojen liikuteltavuutta vähennetään ja voidaan saada suorituskykyhyötyä. [Unity 6.6: lightmapping ja baking](https://docs.unity.com/en-us/engine/6000.6/manual/lighting-overview/direct-and-indirect-lighting/lightmapping/baking-before-runtime/lightmappers)

Liikkuva vartija, siirrettävä esine ja avattava ovi eivät saa jättää virheellistä pysyvää varjoa. Näiden valaisu ja tarvittavat liikkuvat varjot tutkitaan erikseen. Nykyinen koodista ajon aikana syntyvä huone ei vielä sisällä leivottua valaistusta; ulkoasukoe vaatii editorissa säilyvän ympäristön tai muun erillisen leivontatyönkulun. URP säilyy ulkoasukokeen ehdokkaana.

Seuraava taidekoe: yksi viimeistellympi selli, yksi hahmo ja sama keskustelukuvakulma. Mittaa Macilla 1080p:llä CPU/GPU-frameajat, muistikuorma ja ruudunpäivityksen vaihtelu, ensin ilman ja sitten kahden pelaajan kanssa. Alustava 60 fps on tavoite, ei tähän kuvan kaltaiseen laatuun mitattu tulos. Myöhemmin testi tehdään myös valittavalla Windows-kohdelaitteella. Grafiikan rinnalle pitää jäädä kapasiteettia verkkopelille ja myöhemmälle puheketjulle.


## Toteutettu ensimmäinen ympäristö

[Käyttäjän painopisteen mukainen visuaalinen versio 03](VISUAL-START.md) laajentaa aluetta kahteen sellikerrokseen, ruokalaan ja pihaan. Kuvantarkistuksen perusteella hillittiin lattian pintavaihtelua ja vaalennettiin vihreää. Hahmot, animaatiot ja leivottu valaistus eivät vielä sisälly versioon. [Materiaalin alkuperä](references/game-look/GENERATED-ASSETS.md).

## Modulaarisen hahmon koe — 14.10.2026

Uusi käyttäjän referenssi: [nelikon kuva](references/characters/group-style-01.png). Ensimmäisen hahmon kasvojen kulmikkuus ja ilme ovat pääsuunta, eivät tarkka kopio. Eri pituudet, vartalon leveys, hiukset, ikä ja asento tekevät hahmoista tunnistettavia saman tyylin sisällä.

Versio 04 käyttää neljää geometriasta koottua varianttia ja samaa vertex-värimateriaalia. Jaetut mittasuhde- ja väriparametrit mahdollistavat variaatiot ilman jokaisen hahmon erillistä mallinnusta. Liike on nivelosista rakennettu kävelykoe. Nykyinen kasvojen yksityiskohtaisuus, vaatteen istuvuus ja animaatio eivät vastaa referenssin viimeistelyä. Rigattu tuotantohahmo ja parempi kävely ovat myöhemmän taidetyön vaihtoehtoja.

Käyttäjän pyynnöstä tämän kokeen selliosaston vangit ovat vaaleaihoisia. Koko pelin modulaarinen järjestelmä sallii eri ihonsävyt uuden briiffin mukaisesti.

## Käytössä olevan laitoksen ikä — 15.10.2026

Käyttäjän vahvistama suunta: 30–40 vuoden käytön tuntu. Kuluma näkyy käytetyissä kohdissa, eri-ikäisissä maalikerroksissa ja pienissä korjauksissa. Tila on toimiva, ylläpidetty mutta niukka vankila. Vältetään yleistä raunio- tai kauhupeliränsistymistä. Version 06 [devlog](devlog/2026-10-15-maintained-wear.md) kuvaa toteutuksen ja rajat.

## Kolmen kerroksen arkkitehtuuri — 15.10.2026

[Uudet käyttäjän referenssit](references/environments/layout-07/README.md) tarkentavat osaston pitkää perspektiiviä, kolmea kerrosta, ulkonevia vaaleita kerroskäytäviä, katon ylävaloa ja lämpimän sävyistä kalteriterästä. Toisen kuvan vaalea laitosilme säilyy; aikaisempi 30–40 vuoden käyttöjälkien suunta jatkuu. Kolmas kuva ohjaa tavallisia kulkuyhteyksiä: selliosastolta siirrytään ensin valvottuun sisätilaan ja sieltä yardille.

## 15.10.2026 — alkuperäinen hahmoreferenssi, versio 10

Käyttäjä haluaa palata alkuperäisen hahmokuvan ilmeeseen, säilyttäen 1960-luvun vankilaan sopivan vaatetuksen. [Referenssi](references/characters/original-character-reference.jpg).

Tulkinta: pitkähköt kasvot, vahvat kulmat, kulmikas nenä ja poskipäät, suuret korvat, epäsymmetrinen hiustuppo; selkeät murretut värialueet. Vaatteissa avoin kaulus ilman kravattia, pelkistetty rintatasku, käännetyt hihat, suorat housut ja ruskeat kengät. Sininen/harmaa on perusta, khaki oman fiktiivisen laitoksen työvaatevariaatio. Ei väitettä kaikkien 1960-luvun vankiloiden yhtenäisestä univormusta.

Historiallinen ankkuri: [Alcatrazin vankisääntöjen vaatetuskohta](https://www.alcatrazhistory.com/regpage2.htm), joka nimeää chambray-paidan, housut, vyön ja kengät. Tämä ei määrää kaikkia oman pelin leikkauksia, värejä tai hihankääntöjä.

Toteutus: kasvojen yhtenäinen tasoverkko, muotoillut hiussuortuvat, useampi leveys-/syvyyskohta housuissa ja hihoissa, ohuempi tasku/vyö/kaulus ja neljä hieman kaartuvaa sormea. Nykyinen rigi ja animaatiot säilyvät. Pinnan värit ovat verteksivärejä; uusia suuria tekstuureja tai ulkoisia assettipaketteja ei lisätä. Tämä on referenssin suuntaan tehty oma tulkinta, ei kuvan tarkka 3D-rekonstruktio.
