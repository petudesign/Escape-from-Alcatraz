# Backlog v0.2

PC-001 on osittain selvitetty (kone, alustava ajankäyttö ja työtapa); PC-003 on osittain toteutettu (huone, ohjaus, käyttöesine, Mac-build ja paikalliset ajotestit); PC-005 on osittain toteutettu (kaksi erillistä prosessia samalla Macilla ja hostin ratkaisema työkalun omistajuus); muut ticketit ovat tilassa TODO. Ei viety ulkoiseen projektinhallintaan. Omistaja tarkoittaa tarvittavaa työroolia, ei olemassa olevaa työntekijää. AI auttaa tekijää. Prioriteetit: P0 ennen ydinkokeen jatkopäätöstä, P1 ennen demoa, P2 myöhemmin.

## M0 — lähtötilanne

### PC-001 · P0 · Mac ja työskentelyn reunaehdot
Omistaja: tekijä. Riippuvuudet: ei.
Kirjattu 14.10.2026: Mac mini M6 / 24 GB, macOS 27.0.1, noin 329 GiB vapaata levytilaa; käyttäjä voi käyttää useita tunteja päivässä ja ohjaa suunnittelua AI:n avustaessa toteutuksessa. Tarkka viikkotyömäärä, testikaveri ja kulukatto avoimia. Katso docs/START-HERE.md. Hyväksytty: ensimmäisen kokeen tavoite ja resurssit näkyvät README:ssä; maksullisia palveluita ei aktivoida oletuksena.

### PC-002 · P0 · Erillinen peliprojekti ja palautus
Omistaja: tekijä/tekninen. Riippuvuus: PC-001.
Perusta erillinen pelin versionhallinta sovittuun sijaintiin; tämä suunnittelukansio on nyt Notch-työtilan sisällä. Älä luo sisäkkäistä Git-repoa vahingossa. Hyväksytty: projektin saa puhtaasta checkoutista käyntiin; avaimet ja buildit eivät päädy versionhallintaan.

### PC-003 · P0 · Natiivi moottorikoe Macilla — OSITTAIN TOTEUTETTU
Omistaja: tekninen. Riippuvuus: PC-001.
14.10.2026: Unity 6000.6.5f1 -projekti ja ARM64 Mac-build ajettu. Kahdeksan automaattista ajotarkistusta läpäisty. Mikrofonin paikallinen tasotesti on toteutettu, mutta oikea äänen syöttö ja kahden asiakkaan yhteinen tila eivät vielä ole todennettuja. Katso unity/PrisonCoop/README.md. Tuotantomoottori jää avoimeksi kokeiden loppuun asti. Unreal vertailuehdokas, Godot säilyy vaihtoehtona. Katso docs/ENGINE-EVALUATION.md. Hyväksytty: yksinkertainen 3D-huone, liikkuminen, yhteisen esineen tilan koe kahdella asiakkaalla, mikrofonilupa ja ajettava Mac-build. Päätösloki perustelee moottorin. Ei vielä Steam- tai grafiikkapakettia.

### PC-004 · P0 · Miljööpäätös ja historialliset vapaudet
Omistaja: pelisuunnittelu/tutkimus. Riippuvuus: ei.
Täydennä SETTING-RESEARCH.md:n kahdeksan kysymystä. Hyväksytty: oikea Alcatraz vs oma paikka perusteltu, lähteet ja pelilliset poikkeamat eritelty. Nimi pysyy työnimenä oikeustarkistukseen asti.

## M1 — pelin ydinkoe

### PC-005 · P0 · Kahden pelaajan yhteinen tila — OSITTAIN TOTEUTETTU
Omistaja: tekninen. Riippuvuudet: PC-002, PC-003.
14.10.2026: kaksi erillistä prosessia samalla Macilla yhdistetty. Väliaikainen kapselihahmo näyttää toisen pelaajan; yhteinen työkalu, yksittäinen omistaja, tavoitettavuus ja katkeamisen jälkeinen palautus tarkistettu. Eri koneiden lähiverkkotesti ja kaksi ihmistä ovat avoimia. Host ja asiakas näkevät toisensa sekä saman aukon/esineen tilan. Hyväksytty: kaksi asiakasta samassa lähiverkkotestissä; yhden pelaajan muutos näkyy toiselle eikä molempi voi omistaa samaa esinettä. Internet-yhteys testataan myöhemmin erikseen.

### PC-006 · P0 · Mikrofonista puheeksi litterointi
Omistaja: puhetekniikka. Riippuvuus: PC-003.
Vertaa paikallista tunnistusta ja yhtä pilvivaihtoehtoa kohde-Macilla. Hyväksytty: 20 lausetta, mukana suomi/englanti, hiljainen puhe ja nimet; virheet, viive ja CPU-kuorma kirjattu. Puutteellinen kieli ei saa vahingossa näkyä tuettuna.

### PC-007 · P0 · Kuuluvuus ja puhujan identiteetti
Omistaja: tekninen/ääni. Riippuvuudet: PC-005, PC-006.
Proximity-chat ja NPC:lle välittyvä sisältö käyttävät samoja etäisyys-/estekriteereitä. Hyväksytty: kaukana lausuttu salaisuus ei päädy vartijan muistiksi. Eri mikrofonit eivät muuta valittua kuiskaus-/puhetilaa epäreilusti.

### PC-008 · P0 · NPC:n rajattu tieto ja toiminnot
Omistaja: tekninen/pelisuunnittelu. Riippuvuus: PC-005.
Havainnot, suhde, tehtävä ja lyhyt muistiyhteenveto. Rakenteinen mallivastaus ehdottaa vain sallittuja tekoja. Hyväksytty: puheella ei luoda esineitä, ohiteta ovia tai paljasteta toisen NPC:n salaisia havaintoja.

### PC-009 · P0 · Vapaa selitys ja toisen pelaajan keskeytys
Omistaja: tekninen. Riippuvuudet: PC-006, PC-007, PC-008.
Vartijan kysymys → oma vastaus → kaverin harhautus → näkyvä teko. Hyväksytty: puhe muuttaa huomiota pelin sääntöjen rajoissa. Vanhentuneet vastaukset hylätään. Taustapalvelu pitää API-avaimet poissa buildista.

### PC-010 · P0 · Hahmon ääni ja viivemittaus
Omistaja: puhetekniikka/ääni. Riippuvuus: PC-009.
Yksi lisenssiltään soveltuva väliaikainen ääni. Hyväksytty: reaktio ja ääni aikaleimattu; vastaus generoidaan kerran ja kuuluu sijainnin mukaan kaikille. Palvelun viive ei pysäytä koko maailmaa.

### PC-011 · P0 · Lappu
Omistaja: tekninen/UI. Riippuvuus: PC-005.
Piirto hiirellä ja vaihtoehtoinen kirjoitus; fyysinen luovutus. Hyväksytty: vastaanottaja näkee saman sisällön ja omistajuus säilyy. NPC ei tiedä sisältöä ennen pelin hyväksymää lukemistapahtumaa.

### PC-012 · P0 · 20 tapauksen sääntötesti ja ensimmäinen pelitesti
Omistaja: tekijä/QA. Riippuvuudet: PC-009, PC-010, PC-011.
Vaihtele selityksiä, havaintoja, suhteita ja päällepuhumista. Hyväksytty: tulokset ja 10–15 min kahden ihmisen pelitesti kirjattu. Päätä jatko/korjaus/rajauksen muutos ennen M2:ta. Testaa myös yritys käskeä mallia unohtamaan pelisäännöt.

## M2 — pieni pelattava kokonaisuus

### PC-013 · P1 · Tavaran hankinta ja kaksi ratkaisua
Omistaja: suunnittelu/tekninen. Riippuvuus: PC-012 jatkopäätös.
Yksi ostettava tavara ja palveluksella saatava vaihtoehto. Hyväksytty: rahojen menetys muuttaa tehtävää mutta ei lukitse kampanjaa.

### PC-014 · P1 · Epäily näkyy maailmassa
Omistaja: suunnittelu/ääni. Riippuvuus: PC-013.
Yksi vartijan havainto, yksi varoituslappu ja tarkastuksen määräaika. Hyväksytty: pelaajat pystyvät kertomaan uhan ja toimintamahdollisuuden ilman NPC-statuspalkkia.

### PC-015 · P1 · Kiinnijäänyt pysyy pelaajana
Omistaja: suunnittelu/tekninen. Riippuvuus: PC-014.
Kuulustelu ja ryhmään vaikuttava viivytys. Hyväksytty: pelaajalla on vaikuttava tehtävä; kahden pelaajan ryhmä ei lukkiudu. Myös tunnistusvirheen käsittely testattu.

### PC-016 · P1 · Skaalaus 2/3/4 pelaajalle
Omistaja: suunnittelu/verkkotekniikka. Riippuvuudet: PC-013, PC-015.
Ei korvaavia CPU-pelaajia. Hyväksytty: tavoitteet onnistuvat jokaisella ryhmäkoolla ja jokaiselle riittää päätöksiä. Testaa myös yhden yhteyden katkeaminen; hostin vaihto ei ole oletusominaisuus.

### PC-017 · P1 · Yksi lyhyt pakoreitti
Omistaja: kenttäsuunnittelu. Riippuvuus: PC-016.
Hankinta → peitetty valmistelu → tarkastus → pako tai takaisku. Hyväksytty: 20–30 min kokonaisuus, jossa voi onnistua ja epäonnistua ymmärrettävästi.

### PC-018 · P1 · Kolme ulkopuolista ryhmää
Omistaja: tekijä/QA. Riippuvuus: PC-017.
Hyväksytty: eri ryhmäkoot, osallistuminen, epäselvyydet, viiveet ja kulut kirjattu. Kirjaa myös neutraalit/negatiiviset havainnot ja syyt keskeyttämiseen.

## M3 — esiteltävä demo

### PC-019 · P1 · Yksi tunnistettava taidesuunta
Omistaja: taide. Riippuvuus: PC-018 jatkopäätös.
Yksi selli, käytävä, vartija ja vanki muodostavat yhtenäisen kokonaisuuden. Hyväksytty: mallien, tekstuurien, fonttien ja äänien alkuperä/lisenssit kirjattu. AI-kuva ei sellaisenaan ole pelivalmis 3D-malli.

### PC-020 · P1 · Kuultava uhka ja animaation palaute
Omistaja: ääni/animaatio. Riippuvuus: PC-019.
Askeleet, avaimet, paperi, työääni sekä huomion kääntyminen. Hyväksytty: ääni kertoo tilanteesta luotettavasti ja tekstitetty vaihtoehto on käytettävissä.

### PC-021 · P1 · Tallennus, jatkaminen ja Mac-jakelu
Omistaja: tekninen. Riippuvuus: PC-017.
Hyväksytty: tavarat, velat, NPC:n olennainen muisti ja päivävaihe säilyvät; väärä versio havaitaan. Buildin mikrofoni- ja jakeluoikeudet toimivat toisella Macilla. Windows-asiakkaan proof ennen Windows-lupausta.

### PC-022 · P1 · Palvelukulut ja häiriötila
Omistaja: backend. Riippuvuudet: PC-010, PC-018.
Hyväksytty: palvelinpuoliset rajat, katkaisin, autentikointi ja mykistys; katkos ei tee NPC:stä kaikkitietävää tai rankaise pelaajaa. Puheen/datankäsittelyn kuvaus näkyy ennen ulkopuolista käyttöä.

### PC-023 · P1 · Demotesti ja esittelypaketti
Omistaja: tuote/QA. Riippuvuudet: PC-019–PC-022.
Hyväksytty: viisi ryhmää, todellinen video, pelattava build, kustannusmittaus ja vaihekohtainen budjetti. Avoimet riskit ilmoitettu. Ei vielä ulkoisia julkaisuja tai yhteydenottoja ilman käyttäjän ohjetta.

### PC-024 · P1 · Rahoituksen hakukelpoisuus ja aikataulu
Omistaja: tekijä/liiketoiminta. Riippuvuus: PC-001; alkaa rinnakkain tutkimuksena.
Hyväksytty: yritysmuoto, perustamisaika, tiimi, omarahoitus, aiemmat julkaisut ja tukien kustannusajankohdat selvitetty. Päivitä FUNDING.md ennen sitovia hankintoja. Hakua ei merkitä avoimeksi vanhan sivun perusteella.

## Myöhempi sisältö — ei vielä demo-ominaisuuksia

### PC-025 · P2 · Korttipeli ja sosiaalinen seuraus
Omistaja: suunnittelu/tekninen. Riippuvuus: PC-013.
Hyväksytty myöhemmin: luovutetut vaihtotavarat, rajattu pöytätalous, velka tai vaikeampi vaihtoehto. Ei rajatonta rahantekokonetta eikä lopullista lukkiutumista.

### PC-026 · P2 · Kampanjarytmi ja viimeinen viikko
Omistaja: suunnittelu/käsikirjoitus. Riippuvuus: PC-023.
Hyväksytty myöhemmin: uusia päätöksiä avaavat vaiheet, päivän ohitus ja määräajalle syy. 8–12 h ei synny odottamisesta tai pakollisesta toistosta.

### PC-027 · P2 · Väkivallan ja ilmiantojen sisältöpäätös
Omistaja: suunnittelu. Riippuvuus: PC-018.
Hyväksytty myöhemmin: palveluntarjoajien soveltuvuus, pelin sävy, todistajat ja seuraukset testattu. Tappaminen ei ole vielä luvattu ominaisuus. Ei yhtä painiketta julkisen ryhmän koko kampanjan tuhoamiseen.

### PC-028 · P2 · Julkiset ryhmät ja leaderboardit
Omistaja: tekninen/tuote. Riippuvuus: PC-023.
Hyväksytty myöhemmin: tarve osoitettu; moderointi, katkeamiset, potkut, ryhmäkoot ja reilu ajanotto suunniteltu. Pilvimallin muuttuvuus huomioitu kilpailullisissa tuloksissa.

## Uuden briiffin rajaukset — 14.10.2026

PROJECT-BRIEF.md korvaa vanhat yhteisrahaesimerkit: ei party walletia eikä käteistä, henkilökohtainen prison account ja fyysiset vaihtotavarat. PC-025 ei lukitse blackjackia tai oikeuta ryhmän saldon menettämistä; mahdollinen korttipeli liittyy tavaroihin, palveluksiin ja velkoihin. Oma saarivankila on valittu PC-003:n paikka-arvioinnin jälkeen, mutta tarkka historiallinen käytäntötutkimus jatkuu.
