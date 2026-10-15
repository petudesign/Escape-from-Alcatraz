# Kävelevät vangit ja tarkennettu pelibriiffi

14.10.2026. Kirjoittaja: AI-avustaja. Käyttäjän päätökset on erotettu toteutuksen kokeista.

## Käyttäjän pyyntö

Lisää liikkuvia hahmoja näyttävään vankilaympäristöön. Nelikon ensimmäinen kasvotyyli ohjaa suuntaa, hahmot saavat olla eri kokoisia ja näköisiä. Nykyinen selliosasto on pyydetty vaaleaihoiseksi. Verkko- ja mikrofonitestit ovat toistaiseksi sivussa.

Uusi [projektibriiffi](../PROJECT-BRIEF.md) vahvistaa oman maailman ja loren, henkilökohtaiset vankilatilit ja kolme taloutta, päivärytmin, työstä saatavan accessin, itemien tilannepohjaisen käytön, 8–12 tunnin tavoitteen ja epäselvän pakolopun. Historiallisessa käsittelyssä ei käytetä rasistisia haukkumasanoja tai shokkihakuisuutta. Nämä eivät vielä ole toteutettuja järjestelmiä.

## Toteutuksen valinta ja perustelu

Seitsemän NPC:tä käyttää neljää samasta rakenteesta muodostettua hahmovarianttia. Pituus, leveys, iho, hiukset ja vaatteen väri ovat parametreja. Neljä kävelee selliosastolla, yksi yläkerroksessa ja kaksi pihalla. Kulmikkaat kasvot, nenän tasot, kulmakarvat, hiusten isot muodot ja hillitty työvaatetus seuraavat referenssin suuntaa.

Muodostimme ensimmäiset hahmot Unity-geometriasta ja yhteisestä vertex-värimateriaalista. Tämä tekee ulkoasun muuttamisesta nopeaa ilman ulkopuolisia assetteja ja näyttää heti, miten siluetti toimii nykyisessä valossa. Vaihtoehto olisi rigattu ulkopuolinen hahmo ja valmis animaatio; se voisi parantaa liikettä mutta vaatii tyylin, lisenssin ja tuontityön sovittamista.

Kävely on nivelosien kiertoon perustuva koe, ei tuotantotason hahmoanimaatio. Reitit ja lyhyet pysähdykset on määritelty etukäteen. NPC:t hidastavat kohtaamistilanteissa ja pysähtyvät pelaajan lähellä; tämä ei ole varsinainen navigation-, havainto- tai päivärytmisimulaatio. Kasvot ja vaatteet ovat referenssiä yksinkertaisemmat.

## Näyttö ja rajat

Mac-versio 04 käännettiin Unity 6000.6.5f1:llä. Pelin ajettavasta versiosta tallennettiin hahmon lähikuva ja ympäristönäkymät [evidence/characters-04](../evidence/characters-04/character.png). Liikkeen kuvakaappauskoe kirjaa jokaisen NPC:n siirtymän kolmen sekunnin aikana motion.txt-tiedostoon. Kaikki seitsemän liikkuivat: siirtymät olivat 1,818–3,115 metriä. Tämä lyhyt havainto ei todista pitkän ajon reittejä, ruuhkien väistämistä tai suorituskykyä.

Ei uusia verkko- tai mikrofonikokeita. Ei LLM-puhetta, sosiaalista taloutta, työvuoroja tai kampanjan sisältöä. Tuotantolaadun arvio vaatii paremmat hahmomallit/animaatiot ja myöhemmin suorituskykymittauksen.

## Mitä muuttui suunnittelussa?

Vanha avoin Alcatraz-valinta korvattiin omalla saarivankilalla. 10–15 tunnin hypoteesi korvattiin käyttäjän 8–12 tunnin tavoitteella. Yhteisen rahan menettämiseen perustuvat esimerkit poistettiin nykyisestä kuvauksesta. Vanhoja devlogeja ei muutettu: ne säilyttävät aiemmat oletukset case studyta varten.

AI:n ehdotus seuraavaksi on yksi pieni arjen tilanne, jossa työ antaa pääsyn ja palvelus tai tieto avaa valmistelua. Koko saaren ja lopullisen kampanjan toteuttaminen eivät ole tämän hahmokokeen osa.
