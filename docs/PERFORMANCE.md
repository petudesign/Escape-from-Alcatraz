# Suorituskyky ja koko osana suunnittelua

15.10.2026. Käyttäjän pysyvä tavoite: peli pyörii hyvin ja sen koko pysyy kohtuullisena. Tavoite koskee toteutustapaa alusta asti, ei vain julkaisun loppusiivousta.

## Nykyinen käytäntö

- Säilytetään vain uusin tarkistettu Mac-sovellus. Vanha toimiva sovellus poistetaan vasta korvaavan version tarkistusten jälkeen. Dokumentit, lähdekoodi, blueprintit ja mittausnäyttö säilyvät.
- Pienet toistuvat ympäristöosat yhdistetään materiaaleittain ja aluelohkoittain. Koko rakennusta ei yhdistetä yhdeksi aina näkyväksi meshiksi.
- Poistetaan yhdistämisestä jäävät renderöimättömät tyhjät objektit ja turhat käytöstä poistetut colliderit.
- Hahmolla on yhteinen värimateriaali ja nivelkohtaiset meshit; yksittäinen sormi tai kulmakarva ei tarvitse omaa renderer-osaa. Pelaajat jakavat materiaalin, eikä uusi ulkoasu edellytä suurta uutta tekstuuripakettia.
- Nykyinen seinätekstuuri on rajattu 1024 pikseliin. Lattiatekstuurissa on mipmapit ja suodatus. Shaderin tiheä kohina suodatetaan pikselin kattaman pinta-alan mukaan.
- Vain keskeinen käytävävalo ja aurinko tuottavat reaaliaikaiset varjot. Koristevaloista ei tehdä automaattisesti kuutta varjokameraa käyttävää pistevarjovaloa. Pikselivalojen ja varjoetäisyyden määrää rajataan.
- Uudet assetit arvioidaan tyylin lisäksi niiden geometrian, tekstuurimuistin, materiaalien ja lisäpakettien kannalta. Raskasta pakettia ei lisätä yksittäisen pienen tarpeen vuoksi.

## Mitä mitataan?

Ajettavan Mac-version koko, havainnoitu ruutuaika eri näkymissä, muistin käyttö ja kohtauksen renderer-määrä. Ruutuajan mediaani ja 95. persentiili kertovat enemmän kuin yksi hetkellinen FPS-luku. Renderer-määrä ei tarkoita näkyvien draw callien määrää.

Version 08 sisäinen koe käyttää 1440 × 900 -renderöintikohdetta, MSAA 4:ää, samaa shader-/valoasetusta ja yhtä manuaalisesti renderöityä kameraa neljässä näkymässä. VSync ja FPS-rajoitus ovat mittauksen ajaksi pois. Tulokset ovat `docs/evidence/update-08/performance.txt`.

Tämä on lyhyt paikallinen vertailupohja. Se ei ole GPU-profileri, Steam-minimivaatimus, kaikkien koneiden suorituskykylupaus tai monen pelaajan pitkän session testi. Mittaus ei sisällä tulevia puhepalveluita, päivärytmiä tai koko saarta. Varsinainen julkaisuprofilointi tehdään myöhemmin edustavilla Mac- ja Windows-koneilla.

## Suunnittelun työraja

60 FPS / noin 16,7 ms on alustava tavoite; minimilaitteistoa ja laatutasoja ei ole vielä päätetty. Uudelle ominaisuudelle varataan selkeä kustannus. Näkyvän parannuksen pitää perustella lisämuisti tai ruutuaika. Jos kohtaus ei mahdu budjettiin, ensisijaiset säätökohdat ovat näkyvät materiaalit, valot/varjot, turha geometria ja tekstuurikoko.

Poistotyökalu: `tools/keep-latest-build.py`. Se rajaa poistot vanhoihin PrisonCoop-sovelluksiin Builds/macOS-kansiossa ja varmistaa ensin säilytettävän sovelluksen suoritettavan tiedoston olemassaolon.

## Version 08 lähtömittaus

Sovellus 91.91 MiB. Raskaimman mitatun näkymän mediaani 5,20 ms / p95 5,39 ms; muisti 152 MiB. Nämä ovat yllä kuvatun lyhyen renderöintikokeen arvoja, eivät koko valmiin pelin minimilaitevaatimuksia. Vain uusin sovellus säilyy; vanhojen poistolla vapautui noin 720,9 MiB.

## Version 09 mittaus — 15.10.2026

Sovellus 96 380 244 tavua (91,92 MiB), noin 3 KiB enemmän kuin v08. Sama lyhyt renderöintikoe: näkymien mediaanit 5,49 / 2,20 / 1,79 / 0,70 ms ja p95 6,41 / 2,50 / 2,00 / 0,85 ms. Unityn allokoitu muisti 148 MiB, kohtauksen MeshRenderereitä 1221 (+45: kolme niveltä kullekin 15 NPC:lle; paikallinen keho piilotettuna tässä kokeessa). Raakadata: `docs/evidence/update-09/perf/performance.txt`.

Raskaimman näkymän v08 mediaani oli 5,20 ms ja p95 5,39 ms; v09 tässä yksittäisessä otoksessa hitaampi. Mittaus ei erota animaatio-, renderöinti- ja ajokertavaihtelun vaikutuksia, eikä ole FPS-lupaus. Uudet nivelet lisäävät renderereitä, eivät tekstuuri- tai assettipakettien kokoa.
