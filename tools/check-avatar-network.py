"""Two independent local players; tests avatar presence, not Steam or remote LAN."""
from pathlib import Path
import subprocess
import time
root=Path(__file__).resolve().parents[1]
app=root/'unity/PrisonCoop/Builds/macOS/PrisonCoop-10.app/Contents/MacOS/Prison Coop - Cell Test'
out=root/'docs/evidence/update-10/network'
out.mkdir(parents=True,exist_ok=True)
def start(role):
    return subprocess.Popen([str(app),'-batchmode','--avatar-check',role,str(out),'-logFile',str(out/(role+'.log'))],stdout=subprocess.DEVNULL,stderr=subprocess.STDOUT)
host=start('host');time.sleep(1.2);client=start('client')
try:
    client_code=client.wait(timeout=45)
    host_code=host.wait(timeout=15)
except subprocess.TimeoutExpired:
    for process in (host,client):
        if process.poll() is None:process.terminate()
    raise
print(f'Avatar integration: host exit={host_code}, client exit={client_code}')
raise SystemExit(0 if host_code==0 and client_code==0 else 1)
