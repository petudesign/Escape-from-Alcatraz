"""Offline, assumption-driven voice API cost calculator. No external dependencies."""

import argparse
import json
from decimal import Decimal
from pathlib import Path


def calculate(config):
    rates = config["rates"]
    common = config["common"]

    def dec(value):
        number = Decimal(str(value))
        if not number.is_finite() or number < 0:
            raise ValueError("Inputs must be finite and nonnegative")
        return number

    # Validate unused configuration values too, so bad assumptions fail loudly.
    for value in list(rates.values()) + list(common.values()):
        dec(value)
    players = dec(common["players"])
    if players == 0:
        raise ValueError("players must be positive")

    output = []
    for scenario in config["scenarios"]:
        turns = dec(scenario["npc_turns_per_lobby_hour"])
        minutes = dec(scenario["billed_stt_minutes_per_lobby_hour"])
        llm = turns * (
            dec(common["input_tokens_per_turn"])
            * dec(rates["llm_input_per_million_tokens"])
            + dec(common["output_tokens_per_turn"])
            * dec(rates["llm_output_per_million_tokens"])
        ) / Decimal(1_000_000)
        stt = minutes * dec(rates["stt_per_billed_minute"])
        tts = (
            turns * dec(common["spoken_characters_per_turn"])
            * dec(rates["tts_per_thousand_characters"])
            / Decimal(1000)
        )
        variants = {}
        for name, hourly in {
            "local_stt_local_tts_cloud_llm": llm,
            "local_stt_cloud_tts_cloud_llm": llm + tts,
            "cloud_stt_cloud_tts_cloud_llm": llm + stt + tts,
        }.items():
            campaign = hourly * dec(common["campaign_hours"])
            variants[name] = {
                "per_lobby_hour": float(hourly),
                "per_player_hour": float(hourly / players),
                "per_campaign_group": float(campaign),
                "per_campaign_player": float(campaign / players),
                "all_campaign_groups": float(campaign * dec(common["campaign_groups"])),
            }
        output.append({
            "scenario": scenario["name"],
            "components_per_lobby_hour": {
                "llm": float(llm), "stt": float(stt), "tts": float(tts)
            },
            "variants": variants,
        })
    return {
        "checked_on": config["checked_on"],
        "currency": config["currency"],
        "note": config["note"],
        "assumptions": config,
        "results": output,
    }


def main():
    root = Path(__file__).resolve().parents[1]
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--input", type=Path, default=root / "planning/voice-cost-inputs.json")
    parser.add_argument("--output", type=Path)
    args = parser.parse_args()
    result = calculate(json.loads(args.input.read_text(encoding="utf-8")))
    rendered = json.dumps(result, ensure_ascii=False, indent=2) + "\n"
    if args.output:
        args.output.write_text(rendered, encoding="utf-8")
    else:
        print(rendered, end="")


if __name__ == "__main__":
    main()
