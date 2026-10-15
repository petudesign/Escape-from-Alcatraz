# 14.10.2026 — asetukset ja ensimmäinen paikallinen co-op-yhteys

Käyttäjä kokeili ensimmäistä versiota ja vahvisti **liikkumisen ja tavaran nostamisen toimiviksi**. Tämä on käyttäjän havainto, erillään aiemmista automaattisista tarkistuksista. Hän pyysi perusasetukset, erityisesti hiiren herkkyyden, ja jatkamista niiden jälkeen. Mikrofonia ei hänen mukaansa tarvitse vielä testata.

## Toteutetut muutokset ja perustelut

Esc avaa asetussivun: herkkyys, pystysuunnan kääntö, oletusten palautus ja peliin palaaminen. 1,00 × säilyttää ensimmäisen version tuntuman. Valinnat tallentuvat koneelle. Valikko estää oman liikkeen ja katseen ohjauksen; maailmaa ei pysäytetä globaalisti, jotta sama käyttäytyminen sopii co-opiin. Fokusvaihto avaa valikon ja vapauttaa hiiren.

Mikrofonikomponentti poistettiin nykyisen kohtauksen aktiivisista komponenteista. Lähdekoodi säilyy myöhempää koetta varten. Uusi build ei käynnistä mikrofonia tai pyydä mikrofonilupaa.

Unity Netcode ja Unity Transport hoitavat paikalliset yhteydet. Pelaajien sijainnit välitetään pieninä viesteinä; toisen pelaajan hahmo on kapseli. **Host omistaa työkalun tilan:** asiakkaan nostopyyntö tarkistetaan etäisyyden ja esteen perusteella, toinen ei saa jo omistettua työkalua ja katkeaminen palauttaa esineen pöydälle. Tämä on esineomistajuuden koe, ei lopullinen liike- tai inventaariojärjestelmä.

Yhteys on rajattu samaan koneeseen (127.0.0.1). Internet- tai Steam-liittymistä ei lisätty ennen kuin pieni yhteinen tila on todennettu. Ei maksullista verkkopalvelua tai pilvitiliä.

## Mitä kokeet opettivat?

Ensimmäinen valittu NGO 3.1.0 ei sopinut asennetulle editorille: paketin todellinen manifest vaati Unity 6000.7:ää, vaikka yleinen dokumentaatio kuvasi Unity 6 -tukea. Vanhempi 2.7.0 taas törmäsi Unity 6.6:n EntityId-muutokseen. Vaihdettiin Unity 6.6:n dokumentaation listaamaan **2.13.3**-versioon; ratkaistu Transport-versio on **6.6.0**. Näillä build kääntyi. Version sopivuus tarkistetaan manifestista ja ajosta, ei pelkästä yleisesittelyn lauseesta. [Unity 6.6:n pakettisivu](https://docs.unity.com/en-us/engine/6000.6/manual/packages-list/packages-all/pack-preview/com-unity-netcode-gameobjects)

Näkyvän etähahmon tarkistus havaitsi yhteystapahtuman ja ensimmäisen tilapäivityksen välisen viiveen. Hahmon luonti liitettiin myös yhteystapahtumaan ja testiin annettiin rajattu aika odottaa näkyvää hahmoa. Varsinainen omistajuus, seinän läpi noston esto ja katkeamisen palautus toimivat erillisten prosessien kokeessa.

## Näyttö ja rajat

[Paikallinen ajotesti](../evidence/settings-network/smoke-results.txt) tarkistaa asetusten avautumisen ja säilymisen sekä aiemman liikkumisen ja esinetoiminnot. Asetustesti käyttää erillisiä avaimia, joten käyttäjän valinnat säilyvät. [Asetussivu](../evidence/settings-network/settings-view.png) renderöitiin buildista ja tarkistettiin visuaalisesti.

[Hostin](../evidence/settings-network/network-host.txt) ja [asiakkaan](../evidence/settings-network/network-client.txt) integraatiotestit käynnistävät kaksi oikeaa peliprosessia ja tarkistavat yhteyden, etähahmon, esineen yksittäisen omistajan, esteen sekä katkeamisen. Tämä ei korvaa kahden ihmisen co-op-testiä tai eri koneiden verkkotestiä. Liikkeen sulavuus, etähahmojen törmäykset ja uudelleenyhdistäminen ovat vielä avoimia.

## Taidesuunnan uudet lähteet

Käyttäjä tarkensi koko vankilan pääreferenssin pitkiksi vaaleiksi selliriveiksi, vihertäviksi alaseiniksi ja kerroskäytäviksi. Henkilökohtaisen sellin referenssi ja generoitu pelikuva tallennettiin [taidesuuntaan](../ART-DIRECTION.md). Kokonaisilme on tavoite; generoidun kuvan laatu ei vielä todista toimivan pelin suorituskykyä. Kuvan UI-tekstit ja esineet eivät muutu automaattisesti ominaisuuslistaksi.

## Seuraava koe

Käyttäjän herkkyysasetuksen palaute ja kahden ihmisen paikallinen yhteinen tila. Sen jälkeen vartijan toistettava huomio ja näkyvä jälki ennen vapaan puheen integrointia. Taidekoe voidaan pitää rinnalla yhdessä sellissä, jotta moottoria ja tyyliä ei lukita koko saaren tuotannon perusteella.
