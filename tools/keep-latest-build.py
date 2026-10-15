"""Remove only older game app bundles after the new one has passed its checks."""
from pathlib import Path
import argparse
import shutil
parser=argparse.ArgumentParser()
parser.add_argument('name', help='Verified app bundle to retain, e.g. PrisonCoop-08.app')
parser.add_argument('--apply',action='store_true')
args=parser.parse_args()
root=Path(__file__).resolve().parents[1]/'unity/PrisonCoop/Builds/macOS'
keep=root/args.name
if Path(args.name).name!=args.name or not args.name.startswith('PrisonCoop-') or keep.suffix!='.app':
    raise SystemExit('Expected a single versioned PrisonCoop app name')
if not (keep/'Contents/MacOS/Prison Coop - Cell Test').is_file():
    raise SystemExit('Verified app executable is missing; no apps removed')
removed=0
for old in sorted(root.iterdir()):
    if old==keep or not old.is_dir() or old.is_symlink():continue
    if old.name!='PrisonCoop.app' and not (old.name.startswith('PrisonCoop-') and old.suffix=='.app'):continue
    size=sum(file.stat().st_size for file in old.rglob('*') if file.is_file())
    print(('REMOVE ' if args.apply else 'WOULD REMOVE ')+old.name+f' ({size/1048576:.1f} MiB)')
    if args.apply:shutil.rmtree(old)
    removed+=size
print(f'Total older apps: {removed/1048576:.1f} MiB; retained {keep.name}')
