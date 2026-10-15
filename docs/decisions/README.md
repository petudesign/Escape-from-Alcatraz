# Päätösrekisteri

Päivitetty 14.10.2026. Tila kertoo eron käyttäjän linjauksen, aiemman suunnitelman ja AI:n ehdotuksen välillä. Ehdotus ei muutu hyväksytyksi hiljaisuudesta.

| ID | Aihe | Tila | Peruste / mitä seuraavaksi? |
|---|---|---|---|
| D-001 | Kaupallinen Steam-tuote | Käyttäjän vahvistama tavoite | Julkaisulaatua arvioidaan aidossa buildissa; ei vielä julkaisupäivää. |
| D-002 | Tyylitelty, epärealistinen grafiikka | Käyttäjän vahvistama suunta | Annettu kuva on lähtöreferenssi; tarkka toteutus testataan. [Taidesuunta](../ART-DIRECTION.md) |
| D-003 | Tekijä ohjaa suunnittelua, AI auttaa toteuttamaan | Käyttäjän vahvistama työtapa | Koodin tai mallinnuksen opettelu ei ole oma tavoite. Perustelut, vaihtoehdot ja näyttö avataan tekijälle. |
| D-004 | Kahden pelaajan vartijatilanne ensin | Aiempi suunnitelma; tarkennettu ehdotus | Rajaa puheen ja yhteistyön riskin. [Koe](../FIRST-PLAYABLE.md) |
| D-005 | Unity ensimmäiseen moottorikokeeseen | Käyttäjä hyväksyi kokeen aloittamisen | Lähderunko tehty; lopullinen tuotantomoottori odottaa ajettavan kokeen tuloksia. [Vertailu](../ENGINE-EVALUATION.md) |
| D-006 | Oma saarivankila ja lore | Käyttäjän vahvistama 14.10.2026 | Uusi briiffi korvaa aiemman avoimen valinnan. Alcatraz on inspiraatio, ei 1:1-kopio. |
| D-007 | Vapaa kieli, rajatut peliteot | Aiemman suunnitelman lähtökohta | Ymmärrettävä seuraus ja NPC:n rajattu tieto testataan ennen laajentamista. |
| D-008 | Päätösloki + devlog + kokeen todisteet | Käyttäjän vahvistama tarve; toteutettu dokumenttirakenne | Tukee päätösten perustelua ja myöhempää case studyta. |
| D-009 | Karu, ahdas ja käveltävä selli; WC:n takaa alkava pako | Käyttäjän vahvistama suunta | Sellikuva ja kartat tallessa. Mitat, WC:n siirto ja pakoreitin loppu ovat kokeiltavia ehdotuksia. [Tilasuunnitelma](../PRISON-LAYOUT.md) |
| D-010 | Herkkyysasetukset ja mikrofonikokeen siirtäminen myöhemmäksi | Käyttäjän pyyntö; toteutettu | Esc-valikko, tallentuva herkkyys ja pystysuunnan kääntö. Oma ohjaus pysähtyy valikossa, yhteinen maailma jatkuu. |
| D-011 | Sellirivit, kerroskäytävät ja generoitu pelikuva taidesuunnan lähteiksi | Käyttäjän vahvistama referenssisuunta | [Taidesuunta](../ART-DIRECTION.md); tarkka laatu, mittakaava ja UI ovat vielä kokeiltavia. |
| D-012 | Hostin ratkaisema esineomistajuus; sama kone ensin | Toteutettu prototyyppiratkaisu | Unity Netcode -yhteys ja pieni tilaviestikoe. Eri koneet, lopullinen liike ja Steam-liittyminen ovat avoimia. [Devlog](../devlog/2026-10-14-settings-coop.md) |
| D-013 | Painopiste näyttävämpään pelin alkuun; järjestelmäkokeet sivuun | Käyttäjän vahvistama suunta | Uusi visuaalinen vankila-alue; asetukset säilyvät ja verkkokoe ei ole aktiivinen siinä. |
| D-014 | Kaksi kerrosta, 24 selliä, ruokala ja piha | AI:n toteuttama ensimmäinen visuaalinen rakenne | Selkeä laajennus käyttäjän referenssien suuntaan. Ei lopullinen vankila tai historiallinen pohjapiirros. [Versio 03](../VISUAL-START.md) |

| D-015 | Modulaariset kävelevät vangit | Käyttäjän pyytämä koe; toteutettu v04 | Seitsemän NPC:tä, neljä ulkoasuvarianttia. Ensimmäisen referenssikasvon kulmikkuus ohjaa suuntaa; nykyinen osasto vaaleaihoinen käyttäjän pyynnöstä. |
| D-016 | Henkilökohtainen tili ja kolme taloutta | Käyttäjän vahvistama | Ei party walletia, ei buff-itemeitä. Korvaa vanhojen suunnitelmien yhteisrahaesimerkit. |
| D-017 | 8–12 h ja epäselvä pakoloppu | Käyttäjän vahvistama tavoite | Korvaa aiemman 10–15 h hypoteesin. Kesto ei ole mitattu; lehtikohtaus kuuluu kampanjan suunniteltuun loppuun. |
| D-018 | Päivärytmi, työ accessina ja historiallinen käsittely | Käyttäjän vahvistama | [Nykyinen briiffi](../PROJECT-BRIEF.md). Ei rasistisia haukkumasanoja tai shokkihakuisuutta. Järjestelmät eivät ole vielä toteutettu. |

## D-009:n perustelu

Käyttäjä haluaa sellireferenssin niukkuuden ja ahtauden. Ratkaisu on kapea kalustettu moduuli ja WC:n taakse varattu aukko, ei koko vankilan yksityiskohtainen mallinnus. Vaihtoehtona suuri väljä huone olisi helpompi liikkua, mutta se heikentäisi käyttäjän tavoittelemaa tunnelmaa. Tarkka historiallinen mitoitus taas voi vaatia kameralle ja hahmon liikkeelle liikaa kompromisseja.

Hyväksymätön riski on takertuva liikkuminen tai pakokohdan käyttö ilman uskottavaa tilaa. Ensimmäiset mitat ovat AI:n ehdotus, joka muutetaan tarvittaessa. Kävely, WC:n saavutettavuus, jäljen näkyvyys vartijalle ja kahden pelaajan kohtaaminen ovat päätöksen näyttö. Käyttäjän kartat eivät yksin todista valitun pakoreitin historiallista oikeellisuutta.

## D-005:n tämänhetkinen perustelu

**Ongelma:** moottori pitää valita Macilla tehtävään, AI:n avustamaan verkkopelin tuotantoon. Taidesuunta ei vaadi realismia. Tekijä haluaa arvioida pelin toimintaa ja päätösten syitä.

**Vaihtoehdot:** Unity/C#, Unreal/Blueprints+C++ ja aiemman suunnitelman Godot. Unity-kokeen suositus perustuu AI:n mahdollisuuteen muuttaa ja tarkistaa tekstimuotoisia skriptejä. Unrealin verkko ja visuaalinen käyttäytymisen rakentaminen puoltavat sen pitämistä vertailussa.

**Hyväksymättömät haitat:** Unityn editoriasetusten ja assettien automatisointi on vielä todentamatta. Unrealin AI-työnkulku ja muistikuorma ovat myös mittaamatta. Tämä oli alkuperäisen vertailun tila. Unityssa on sittemmin rakennettu ja ajettu Mac-prototyypit; mikrofonikoe on käyttäjän toiveesta lykätty.

**Päätöksen avaava näyttö:** pieni Mac-build, kaksi asiakasta, yhteinen esine ja mikrofonin havainto. Jos ensimmäinen ehdokas ei läpäise tai vaatii liikaa manuaalista työtä, kokeillaan vertailuehdokasta. Lopullinen moottorivalinta kirjataan omaksi päivätyksi päätökseksi.

Kun kanta muuttuu, lisää uusi merkintä ja viittaa korvattuun kantaan. Säilytä vanha perustelu ja alkuperäinen päiväys.

## D-019 — Hahmon ulkoasu ja liike erotetaan — 15.10.2026

Käyttäjän tavoite: ensimmäisestä näkymästä näyttävämpi, hahmojen kohtaamisjumi pois ja myöhempi laajentaminen mahdolliseksi. Toteutuksen ratkaisu: ulkoasuassetit, PrisonerVisual, PrisonerMotor ja niitä yhdistävä WalkingPrisoner. Päivärytmi voi myöhemmin antaa reittejä; se ei vielä ole toteutettu. [Moduulien vastuut](../MODULARITY.md).

Vanha molemminpuolinen pysähtyminen tuotti lukkiutumisen. Uusi sääntö ennakoi toisen kulkijan ja ohjaa oikealle, arvioi sivusuuntia ja säilyttää törmäykset. Tämä on AI:n valitsema rajattu korjaus nykyiseen tilaan. Vaihtoehtoinen NavMesh-pohjainen maailmanlaajuinen polunhaku tulee arvioitavaksi, kun lukitut ovet ja huoneiden väliset työreittien tarpeet ovat olemassa. Pelkkä kolmen sekunnin siirtymä ei todistanut aiemman version liikenteen toimivuutta; uusi näyttö sisältää pakotetun vastakkaisen kohtaamisen ja pidemmän reittiajon.

## D-020 — 30–40 vuoden käytön kuluma — 15.10.2026

Käyttäjän vahvistama taidesuunta: vanha mutta yhä käytössä oleva vankila. AI:n toteutus kohdistaa hankauksen kulkureiteille, kalusteiden ja ovenpielten lähelle sekä metallin kosketuspintoihin. Ylläpidon historia näkyy harvoina paikkamaalauksina. Ei koko ympäristön muuttamista raunioituneeksi tai kauttaaltaan likaiseksi. Käyttöikä on tunnelman lähtökohta, ei rakennuksen lukittu lore-vuosiluku. [Perustelut ja näyttö](../devlog/2026-10-15-maintained-wear.md).

## D-021 — Kolme kerrosta ja valvottu pihayhteys — 15.10.2026

Käyttäjä korjasi aiempaa rakennetta referensseillä: kolme sellikerrosta, suurempi yard, erillinen siirtymä pihalle sekä esteettömät ja väljät portaat. Nämä korvaavat D-014:n kahden kerroksen kokeen nykyisenä rakennelinjauksena. Kolme kerrosta ja kulun periaate ovat käyttäjän vaatimuksia; 60 selliä, 1,95 m porrasleveys ja 32 × 24 m yard ovat AI:n mitoitusehdotuksia.

Oma porrastila ja vain lepotasanteisiin ulottuvat välipohjat korjaavat käyttäjän havaitseman tukoksen. Aiempi kuvallinen tarkistus ei riittänyt portaiden todentamiseen; uusi pelaajakapselin kulkukoe sisältää nousut ja laskut. [Blueprint](../PRISON-LAYOUT.md) ja [devlog](../devlog/2026-10-15-layout-three-tiers.md).

## D-022 — Näkyvä pelaajakeho ja erottuvat vangit — 15.10.2026

Käyttäjän vaatimus: omat kädet ja jalat sekä muille näkyvä hahmo. Kahdeksan ulkoasudataa korvaa neljän lähes samanlaisen hahmon kokeen. Kravattimainen pitkä tumma paitaraita poistetaan. Paikallinen kamera näyttää raajat ja piilottaa pään/ylävartalon renderöinnin; verkkopari näkee koko hahmon. Ensimmäisen persoonan lepokäsiasento ja ulkopuolelta nähtävä rento asento ovat erillisiä alustavia poseja; ne eivät ole valmis yhteinen tartunta-/IK-animaatio.

## D-023 — Vartiointipisteet ja avoin porras — 15.10.2026

Käyttäjä haluaa vartijoita vankeja vähemmän ja pääosin paikallaan vartioimaan. AI:n ensimmäinen mitoitus: 12 vankia ja kolme vartijaa. Porras siirtyy osaston avoimeen päätyyn; vartiointipisteet ja geometrinen näköyhteys tarkistetaan erikseen. Ruokala kasvaa noin 22 × 18 metriin. Mitat ja suhdeluku ovat toteutusehdotus, eivät historiallinen väite. Vartijoiden havaitsemis-/seuraamusjärjestelmää ei vielä toteuteta tässä visuaalisessa vaiheessa.

## D-024 — Optimointi ja vain uusin pelisovellus — 15.10.2026

Käyttäjän pysyvä vaatimus: kohtuullinen koko ja hyvä suorituskyky huomioidaan kaikessa työssä. Vanhat Mac-sovellukset poistetaan aina korvaavan version tarkistuksen jälkeen. Lähdekoodi, devlogit ja päätösten näyttö säilyvät. [Mittaus- ja toteutuskäytäntö](../PERFORMANCE.md), [devlog](../devlog/2026-10-15-avatars-guards-optimization.md).

## D-025 — Koko oma keho ja elävämpi kävely — 15.10.2026

Käyttäjän vaatimus: luonnollisemmat kädet/jalat ja kävely; myös vartalon tulee näkyä alas katsoessa. AI:n toteutus: säilytetään kevyt modulaarinen geometria, erotetaan rintakehä ja nilkat omiksi niveliksi, sidotaan askelrytmi nopeuteen ja pehmennetään liikkeen alkaminen/loppuminen. Korvaa D-022:n paikallisen ylävartalon piilotuksen ja jatkuvasti kohotetut kädet. Vain oma pää on varjona. Kameran alla keho siirtyy 22 cm taakse näkyvyyden vuoksi; etähahmo säilyttää keskitetyn asennon. Tasaisen alustan pohjakorkeus kompensoidaan nivelistä; tarkka jalan lukitus ja maaston IK ovat myöhempää työtä. Ei uusia assettipaketteja tai tekstuureja.

## D-026 — Alkuperäisen hahmon muotokieli — 15.10.2026

Käyttäjän valinta: alkuperäisen kuvan kasvo- ja pintatyyli, aikakauden vankilavaatetus. AI:n tulkinta: yhtenäiset kasvojen tasot erillisten palikoiden sijaan, muotoillut hiukset ja vaatteet, siniharmaa chambray-henkinen pääpaletti. Khaki-variaatio on fiktiivisen laitoksen design-valinta. Sama kevyt nivelrakenne ja jaettu materiaali. [Taidesuunta](../ART-DIRECTION.md) ja [devlog](../devlog/2026-10-15-character-reference.md).
