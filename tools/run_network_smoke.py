"""Run two copies of the local Mac build; no external services or microphone."""
from pathlib import Path
import subprocess
import time

root = Path(__file__).resolve().parents[1]
binary = root / "unity/PrisonCoop/Builds/macOS/PrisonCoop-02.app/Contents/MacOS/Prison Coop - Cell Test"
evidence = root / "docs/evidence/settings-network"
evidence.mkdir(parents=True, exist_ok=True)
processes = []
try:
    for role in ("host", "client"):
        command = [str(binary), "-batchmode", "--net-smoke", role, str(evidence),
                   "-logFile", str(root / f"unity/network-{role}.log")]
        processes.append((role, subprocess.Popen(command, stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)))
        if role == "host":
            time.sleep(0.5)
    failed = False
    for role, process in processes:
        code = process.wait(timeout=45)
        print(f"{role}: exit {code}")
        result = evidence / f"network-{role}.txt"
        if result.exists():
            print(result.read_text())
        failed |= code != 0
    raise SystemExit(1 if failed else 0)
finally:
    for _, process in processes:
        if process.poll() is None:
            process.terminate()
            try:
                process.wait(timeout=5)
            except subprocess.TimeoutExpired:
                process.kill()
