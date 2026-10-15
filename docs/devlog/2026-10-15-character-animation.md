# Hahmoanimaatio ja oma keho — 15.10.2026

## Käyttäjän havainto ja vaatimus
Kävely oli elotonta, omat kädet/jalat epäluonnollisia ja alas katsoessa vartalo puuttui.

## Löydetty syy
Paikallisen hahmon Body-renderer oli ShadowsOnly, mikä piilotti paidan lisäksi lantion ja vyön. Kädet pysyivät -50/-60 asteen olkapää-/kyynärasennossa. Vanha kävelyvaihe eteni tavoitenopeudella myös hidastettaessa.

## AI:n toteutus ja peruste
Vain pää piilotetaan. Koko paikallinen keho sijoittuu 22 cm kameran taakse, jotta kaulus ei tuki näkymää. Kädet lepäävät sivuilla ja heilahtavat vastakkaisen jalan tahdissa. Paikallinen liikesuunta vaikuttaa jalkojen etu-/sivusuuntaan; myös peruutus huomioidaan.

Rintakehä ja nilkat ovat nyt erilliset nivelet. Kuljettuun nopeuteen sidottu vaihe, pehmennetty liikevoimakkuus, polven koukistus, nilkan vastakierto, sivuttaispainonsiirto ja ylävartalon vastakierto korvaavat aikaisemman yhtenäisen heilurin. Hengitys tuo pienen levossa näkyvän liikkeen. Alin kengänpohja määrittää kehon korkeuden tasaisella alustalla.

Nykyinen modulaarinen geometria säilyy. Kolme lisäniveltä/rendereriä hahmoa kohti; jaetut materiaalit, ei uusia tekstuureja, animaatiopaketteja tai per-frame taulukkoallokaatioita liikkeen laskennassa.

## Tarkistus ja rajat
Standalone-build onnistui. Kahdeksan eri kävelyvaiheen kuvaa ja paikallisen kehon kuvat tarkastettiin. Ajonaikainen koe varmistaa näkyvän vartalon/lantion ja yli 25 asteen polviliikealueen (`../evidence/update-09/animation.txt`). Paikallinen kahden prosessin avatar-koe läpäisi molemmilla puolilla; viimeinen pohjakorkeuden korjaus ei muuta verkkokoodia.

Tämä on edelleen proseduraalinen jäykistä osista koostuva animaatio. Ei vielä skinnattua rigia, jalan maailmanpaikkalukitusta, portaiden maasto-IK:ta tai käden esinetartuntaa. Kuvat ja niveltesti eivät yksin osoita täysin luonnollista liikettä tai liukumattomia jalkoja. Käyttäjän pelituntuma on seuraava arvio.

## Version 09 mittaus — 15.10.2026

Sovellus 96 380 244 tavua (91,92 MiB), noin 3 KiB enemmän kuin v08. Sama lyhyt renderöintikoe: näkymien mediaanit 5,49 / 2,20 / 1,79 / 0,70 ms ja p95 6,41 / 2,50 / 2,00 / 0,85 ms. Unityn allokoitu muisti 148 MiB, kohtauksen MeshRenderereitä 1221 (+45: kolme niveltä kullekin 15 NPC:lle; paikallinen keho piilotettuna tässä kokeessa). Raakadata: `docs/evidence/update-09/perf/performance.txt`.

Raskaimman näkymän v08 mediaani oli 5,20 ms ja p95 5,39 ms; v09 tässä yksittäisessä otoksessa hitaampi. Mittaus ei erota animaatio-, renderöinti- ja ajokertavaihtelun vaikutuksia, eikä ole FPS-lupaus. Uudet nivelet lisäävät renderereitä, eivät tekstuuri- tai assettipakettien kokoa.
