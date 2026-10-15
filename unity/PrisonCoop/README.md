# Prison Coop — ensimmäinen Unity-tilakoe

Ensimmäinen ajettava tilakoe, rakennettu ja tarkistettu **Unity 6000.6.5f1 / Apple Silicon** -editorilla käyttäjän Macilla. Mac-build, asetusten tallentuminen sekä kahden paikallisen asiakkaan yhteys ja esineomistajuus tarkistettu 14.10.2026. Käyttöliittymä nähty myös avatusta sovelluksesta. Käyttäjä vahvisti liikkumisen ja tavaran nostamisen toimiviksi. Päivityksessä on asetukset ja samalla Macilla toimiva kahden asiakkaan verkkokoe. NPC-puhetta tai kahden ihmisen co-op-testiä ei vielä ole.

## Asennus ja avaaminen

1. Käyttäjän asentama **Unity 6.6 / 6000.6.5f1 Apple Silicon** on tämän projektin testattu editori. Aiempi 6.3 LTS -ehdotus oli ennen asennusta tehty lähtökohta. Tämän kokeen vuoksi ei tarvitse asentaa toista editoria.
2. Mac Build Support löytyy asennuksesta ja build onnistui. Lisämoduuleja ei tarvita tähän kokeeseen.
3. Hub → **Projects → Add → Add project from disk**. Valitse tämä **PrisonCoop**-kansio (se, jossa ovat Assets, Packages ja ProjectSettings).
4. Avaa projekti asennetulla 6000.6.5f1-editorilla. Ensimmäinen avaus hakee Input System -paketin ja tuo tiedostot; verkkoyhteys tarvitaan. Jos Unity pyytää ottamaan Input Systemin käyttöön ja käynnistämään editorin uudelleen, valitse **Yes**.
5. Unityn ylävalikosta **Prison Coop → Open Cell Test**, sitten **Play**. Kohtaus luodaan automaattisesti ensimmäisellä tuonnilla; käsin sijoitettavia esineitä tai komponentteja ei tarvita.

Jos Input System -kysely ei tule ja ohjaus ei toimi, asetus on **Edit → Project Settings → Player → Other Settings → Active Input Handling: Input System Package (New)**. Editorin uudelleenkäynnistys tarvitaan.

## Ohjaus, asetukset ja paikallinen co-op

WASD liikkuu, hiiri kääntää katsetta. E ottaa pöydän työkalun ja palauttaa sen. **Esc avaa asetukset**: hiiren herkkyys 0,25–3,00 ×, pystysuunnan kääntö, oletusten palautus ja paluu peliin. Herkkyys 1,00 × vastaa ensimmäisen version tuntumaa. Valinnat tallentuvat tälle koneelle. Valikko pysäyttää oman ohjauksen; se ei pysäytä toisen pelaajan maailmaa. Fokusvaihto avaa asetukset ja vapauttaa hiiren.

Mikrofonikoe on käyttäjän toiveesta siirretty myöhemmäksi. Mikrofonikomponentti ei ole käytössä nykyisessä kohtauksessa eikä peli pyydä mikrofonilupaa.

**Nykyinen Mac-versio:** `Builds/macOS/PrisonCoop-02.app`. Ensimmäinen `PrisonCoop.app` säilyy aiempana kokeena. Jos vanha sovellus on auki, sulje se ennen uuden avaamista. Mac-build syntyy myös Unityn valikosta **Prison Coop → Build Mac Test**.

### Kaksi asiakasta samalla Macilla

1. Avaa uusi Mac-versio ja paina **H**: se aloittaa hostin paikallisesti.
2. Avaa toiseksi asiakkaaksi Unity-editorin sama kohtaus ja paina Play, sitten **J**. Molemmilla tulee olla sama ajantasainen projektiversio. Vaihtoehtona kaksi erillistä build-prosessia.
3. Kumpikin alkaa omasta sellistä. Toinen pelaaja näkyy väliaikaisena kapselihahmona. E-käyttö pyytää hostilta työkalun; esineellä voi olla vain yksi omistaja.
4. Jos työkalun pitäjä katkaisee yhteyden, host palauttaa työkalun pöydälle.

Yhteys käyttää osoitetta **127.0.0.1:7777**, joten tämä on saman koneen koe. Kaverin eri kone, internet, Steam-liittyminen, uudelleenyhdistäminen ja hostin vaihto eivät vielä ole toteutettuja. Liike perustuu asiakkaan lähettämään sijaintiin ja väliaikaiseen kapselihahmoon; muiden hahmojen törmäykset ja liikkeen viimeistely tulevat myöhemmin. Host ratkaisee työkalun omistajan, tavoitettavuuden ja seinän läpi nostamisen eston.

Automaattinen kahden prosessin koe: projektin juuresta `python3 tools/run_network_smoke.py`. Se käyttää erillistä paikallista testiporttia 7799 ja sulkee omat testiprosessinsa.

## Miksi tämä toteutus?

Koe käyttää Unityn sisäänrakennettua renderöintiä ja yksinkertaisia kuutioita. Näin ensimmäinen ajo ei tarvitse taidepakettia tai URP-asetuksia. Tämä on poikkeama alustavasta URP-ehdotuksesta, ei lopullinen renderöintipäätös. Taidesuuntakoe ja URP:n arvio tehdään myöhemmin.

Huone syntyy yhdestä koodirungosta: geometria on helppo muuttaa AI:n avulla ja sama lähtötilanne syntyy joka ajolla. Yksinpelissä käteenotto on paikallinen. Verkkokokeessa Unity Netcode hoitaa yhteydet ja host ratkaisee esineen omistajuuden. Tämä pieni viestimuotoinen koe ei vielä ole koko tuotantopelin inventaario- tai liikejärjestelmä.

Projektikansioon ei ole luotu sisäkkäistä Git-repoa. Se kuuluu tällä hetkellä suunnittelukansion kautta Notch-työtilan versionhallintaan. Erottaminen tehdään ennen tuotannon laajentamista. Unityn generoimat Library-, Temp- ja build-kansiot on rajattu versionhallinnan ulkopuolelle.

## Todennetut tarkistukset 14.10.2026

Mac-build on ARM64 ja sen Info.plist sisältää mikrofonin käyttökuvauksen. [Automaattisten tarkistusten tulokset](../../docs/evidence/2026-10-14/smoke-results.txt) sekä [sellikuva](../../docs/evidence/2026-10-14/cell-view.png) ja [käytäväkuva](../../docs/evidence/2026-10-14/corridor-view.png) ovat tallessa. Testit tarkistivat seiniin törmäämisen, molempien sellien saavutettavuuden, työkalun käteenoton/palautuksen ja käyttötoiminnon valintasäteen. Kuvissa näkyy kameran renderöinti ajettavasta buildista.

Asetusten ja verkkokokeen tulokset: [paikalliset tarkistukset](../../docs/evidence/settings-network/smoke-results.txt), [host](../../docs/evidence/settings-network/network-host.txt), [asiakas](../../docs/evidence/settings-network/network-client.txt) ja [asetusruudun kuva](../../docs/evidence/settings-network/settings-view.png). Automaattinen asetustesti käyttää erillisiä testiasetuksia eikä muuta käyttäjän valintoja. Mikrofonin ja kahden ihmisen co-op-testi ovat avoimia.

## Käyttäjän ensimmäisen ajon tarkistus

- Ei Console-käännösvirheitä tai puuttuvia skriptejä.
- Molempiin selleihin pääsee; seinät ja kalterit estävät läpikulun.
- Hiiri vapautuu Escillä ja fokusvaihdossa.
- Työkalun otto ja palautus toimivat, myös näkökulman vaihtamisen jälkeen.
- Esc avaa asetukset ja valikosta voi palata peliin; herkkyys säilyy uudelleenkäynnistyksessä.
- Mac-build käynnistyy ja materiaalit näkyvät.

Tulokset kirjataan projektin devlogiin. Kahden erillisen prosessin paikallinen yhteys on automaattisesti tarkistettu; seuraava toiminnallinen koe on kahden ihmisen yhteinen pelitilanne.

Lähteet: [Hubin editoriasennus](https://docs.unity.com/en-us/hub/add-editor), [moduulit](https://docs.unity.com/en-us/hub/add-modules), [Input Systemin käyttöönotto](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.17/manual/Installation.html).
