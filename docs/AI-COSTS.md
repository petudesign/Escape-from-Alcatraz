# Puhetekoälyn kustannukset ja ilmainen kokeilu

Tarkistettu 14.10.2026. USD, ei ALV:tä tai valuuttamuunnosta. Hinnat voivat muuttua. Laskelma on esimerkki kuormasta, ei tarjous eikä mitattu peli. Valitun mallin saatavuus, kielituki, lisenssit ja fiktiiviseen väkivaltaan liittyvät sisältöehdot tarkistetaan ennen integraatiota.

## Mitä maksetaan?

Pelaajien keskinäinen VOIP ja NPC:n tekoäly ovat eri kustannuksia. NPC-puhe koostuu tunnistuksesta (STT), keskustelumallista (LLM) ja tuotetusta äänestä (TTS). Lisäksi oma taustapalvelu, peliverkon mahdollinen relay, lokitus ja ylläpito.

Hinnoitteluesimerkki: Googlen hinnastossa Gemini 2.5 Flash-Lite -tekstisyöte on $0,10 / miljoona tokenia ja ulostulo $0,40 / miljoona tokenia. Tätä käytetään vain laskennan vertailuhintana; mallia ei ole valittu. Ilmaiskiintiöitä on, mutta kapasiteetti ja käyttöehdot on tarkistettava omalle tilille ja alueelle. [Google](https://ai.google.dev/gemini-api/docs/pricing)

Deepgramin listalla Nova-3 Monolingual -streaming-tunnistuksen tavallinen hinta on $0,0077/min; sivulla on myös tilapäinen ale. Aura-2-ääni maksaa $0,030 / 1 000 merkkiä. Käytämme laskelmassa tavallista STT-hintaa. Sivulla tarjotaan $200 kokeilukrediittiä. [Deepgram](https://deepgram.com/pricing)

## Peruskuorma: yksi neljän pelaajan lobby-tunti

- Yhteensä 120 NPC-vastausta koko ryhmälle, ei jokaiselle pelaajalle erikseen.
- Jokaisessa pyynnössä 3 000 input-tokenia: ohjeet, hahmon tiedot, muistiyhteenveto ja puhe. Kasvava koko keskusteluhistoria ei ole ilmainen.
- Jokaisessa vastauksessa 120 output-tokenia, mukana puhe ja rakenteinen toimintatulkinta.
- NPC:n lausuma ääni keskimäärin 240 merkkiä/vastaus. Tämä on erillinen oletus; merkkejä ja tokeneita ei pidetä samana yksikkönä.
- Laskutettavaa tunnistusääntä yhteensä 30 min. Jos neljää mikkiä lähetetään jatkuvasti tunnin ajan, tunnistusmäärä onkin 240 min. VAD:n olemassaolo ei takaa ilmaista hiljaisuutta: tarkistetaan palvelun laskutus ja lähetetyn virran toteuma.

| Toteutus | Muuttuva API-kulu / lobby-tunti |
|---|---:|
| Paikallinen STT + paikallinen TTS + pilvi-LLM | $0,04176 |
| Paikallinen STT + pilvi-TTS + pilvi-LLM | $0,90576 |
| Kaikki kolme pilvessä, 30 min STT | $1,13676 |
| Kaikki pilvessä, neljä jatkuvaa äänivirtaa | $2,75376 |

Paikallinen käsittely poistaa tässä vain kyseisen API-laskun. Se kuluttaa pelaajan konetta, lisää kehitystyötä ja vaatii äänimallien oikeuksien tarkistuksen. Ääni generoidaan kerran ja välitetään kuulijoille; samaa vastausta ei generoida neljästi. Macin suorituskyky on testattava.

Kaikki-pilvessä-peruskuormalla 10 h kampanja = $11,3676 per ryhmä, noin $2,8419 per pelaaja neljän ryhmässä. Tuhat tällaista ryhmää = $11 367,60. Ei sisällä uusintapelejä, palvelimia, veroja tai tukea. Kahden pelaajan kuormaa ei oleteta automaattisesti puoleksi: NPC-keskustelujen määrä ratkaisee.

## Voiko aloittaa ilmaiseksi?

1. Suljettu koe: paikallinen puheentunnistus ja omat väliaikaiset äänenäytteet tai lisensoitu paikallinen puhesynteesi; tekstimallin kokeilukiintiö. Whisper.cpp tukee paikallista tunnistusta myös Apple Siliconilla. [Projektin dokumentaatio](https://github.com/ggml-org/whisper.cpp)
2. Pilvipuhekoe: kokeilukrediitit voivat kattaa pienen testin. Asetetaan silti sovelluksen oma istunto-/päiväkohtainen kova raja ja tutkitaan palvelun rajat. Maksullista tiliä ei aktivoida automaattisesti.
3. Julkinen kaupallisen pelin demo: ilmainen lataus ei tarkoita ei-kaupallista käyttöä. ElevenLabs Free ei sisällä kaupallista lisenssiä; oikeudet tarkistetaan valitulle toteutukselle. [ElevenLabsin ohje](https://help.elevenlabs.io/hc/en-us/articles/13313564601361-Can-I-publish-the-content-I-generate-on-the-platform)
4. ElevenLabs Startup Grants tarjoaa hakemuksesta 12 kk ja 33 miljoonaa merkkikrediittiä. Hyväksyntää ei oleteta eikä krediittejä lasketa palkkarahoitukseksi. [Ohjelma](https://elevenlabs.io/startup-grants)

## Ennen julkista demoa

API-avaimet taustapalveluun, tilakohtainen autentikointi, kutsu- ja tokenrajat, katkaisin sekä poikkeustilan lyhyet valmiit vastaukset. Sovellus ei saa luvata NPC:n tekoa ennen pelipalvelimen hyväksyntää. Puheen loppu, ensimmäinen reaktio ja ensimmäinen ääni aikaleimataan. Vanhentunut vastaus hylätään, jos vartijatilanne on jo muuttunut.

Pelaajalle kerrotaan, mitä puhetta/tekstiä lähetetään ulkopuolelle. Kerätään vain tarpeellinen; testilokeihin ei oletusarvoisesti raakamikrofonia. Säilytysaika, alue ja palveluehdot tarkistetaan ennen ulkopuolista testiä. Pelaajien keskinäinen yksityinen puhe ei päädy NPC-mallille, ellei NPC sitä pelin kuuluvuussääntöjen mukaan kuule.

Tuotteen kertamaksu vs jatkuva pilvikulu ratkaistaan mittaamalla. Ei vielä lupausta rajattomasta ikuisesta AI-puheesta tai täysin paikallisesta varatilasta.

## Laskennan päivittäminen

Muokkaa `planning/voice-cost-inputs.json` ja aja projektikansion sisältä:

```sh
python3 tools/voice_costs.py --output planning/voice-cost-results.json
```

Laskuri ei käytä verkkoa, API-avaimia tai maksettuja palveluja. Epävarmat oletukset ja lähdepäivä näkyvät syötteessä.
