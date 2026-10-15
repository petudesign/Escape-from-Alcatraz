# 14.10.2026 — kahdesta sellistä visuaaliseen vankila-alueeseen

## Käyttäjän suunnanmuutos

Käyttäjä ei halunnut jatkaa asetusten, verkon tai mikrofonin testausta. Hän halusi rakentaa näyttävämpää pelin alkua ja enemmän vankilaa kuin kahden sellin kokeessa. Hän raportoi myös epäonnistuneen liittymisen; mahdollinen puuttuva host jäi tulkinnaksi, koska tilanteen syytä ei todennettu. Verkkokoe siirrettiin sivuun eikä käyttäjää pyydetty uusimaan sitä.

## Toteutettu kokonaisuus

AI rakensi oman [visuaalisen kohtauksen](../VISUAL-START.md): 24 selliä kahdessa kerroksessa, pääkäytävä, kerroskäytävät, kaiteet, portaat, ruokala ja pieni ulkoilupiha. Käyttäjän aiemmat arkkitehtuuri- ja värireferenssit ohjasivat toistuvia sellirivejä ja vaaleaa/vihreää seinäjakoa. Pienet esineet tuovat asutun tilan tuntua ilman jatkuvaa tavaratulvaa.

Selli kasvoi kokeiltavaan 3 × 4 m moduuliin. Syynä on vuoteen, WC:n, pesualtaan, pöydän ja käveltävän reitin sovitus. Tämä on AI:n toteutusratkaisu, ei käyttäjän hyväksymä lopullinen mitta eikä historiallinen sellikoko. Suljettu ritilä säilyttää pakoreitin visuaalisen lähtökohdan; sen avaaminen ei ole toteutettu.

Koko saaren sijaan pihalta näkyy vesitornin siluetti ja taustameri. Tämä antaa paikalle mittakaavaa mutta pitää ensimmäisen visuaalisen alueen rajattuna.

## Materiaali- ja valaistuspäätökset

AI loi Imagegenillä kuluneen vaalean seinätekstuurin ja tallensi sen projektin Art/Textures-kansioon. Sama pinta sävytetään vihreäksi alemmissa seinissä. Lattian pienet pintamuutokset ja saumarytmi tuotetaan koodista. Metallin, puun, posliinin ja kankaan materiaalit ovat vielä yksinkertaisia.

Valo perustuu nykyisessä versiossa taivaan ympäristövaloon, rajattuihin käytännön valaisimiin ja aurinkoon. Vain rajatut valot tuottavat reaaliaikaisia varjoja. Pehmeä kuvankäsittely lisää kevyen hehkun ja hillityn reunatummennuksen. Leivottua valaistusta ei vielä ole, eikä suorituskykyä väitetä mitatuksi tämän työn perusteella.

Toistuva geometria yhdistetään materiaalin ja sijaintialueen mukaan. Pienet alueet pitävät valaisinten vaikutuksen ja näkymän rajauksen käyttökelpoisina; koko vankilan yhdistäminen yhdeksi meshiksi tekisi valaistuksen ja näkyvyyden hallinnasta karkeaa. Yhdistetyt meshit ja materiaalit säilytetään nativeina Unity-asseteina, eikä maailma ole enää vain runtime-koodin tilapäinen näkymä.

## Visuaalisen tarkistuksen korjaukset

Ensimmäisissä renderöinneissä sellinumerot olivat liian suuria ja ruokalan/pihan kyltit näkyivät väärinpäin. Numerot pienennettiin ja kyltit käännettiin. Seinäalavihreä oli liian tumma ja lattian pintavaihtelu liian voimakas; niitä hillittiin. Korjauksia perusteltiin omalla kuvantarkistuksella, ei pelaajapalautteella.

## Todentaminen ja seuraava arvio

Version 03 ARM64 Mac-build valmistui onnistuneesti. Sellin, pääkäytävän, yläkerroksen, ruokalan ja pihan näkymät renderöitiin buildista ja tarkistettiin. Näkymät ovat [talteen tallennettuja kuvia](../evidence/visual-prison-03/capture.txt), eivät konseptigenerointia.

Käyttäjän seuraava arvio koskee tilan tuntua ja ilmettä: onko selliosasto oikeansuuntaisen karu, ovatko tilat liian väljiä ja mikä osa kaipaa eniten viimeistelyä? Nykyinen alue ei vielä sisällä vartijaa, hahmoanimaatioita tai kampanjan tehtäviä. Seuraava taideaskel voi olla yksi hahmo ja yksi tarkemmin viimeistelty selli tämän alueen sisällä.
