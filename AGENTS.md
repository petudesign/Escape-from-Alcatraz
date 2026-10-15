# Project working conventions

- This is the user's native Unity prison co-op game. Keep the current design source in docs/PROJECT-BRIEF.md and the playable-area instructions in docs/VISUAL-START.md.
- Preserve user decisions, AI proposals and measured evidence as separate things in the decision log and dated devlogs. The user wants explanations suitable for a later case study.
- Consider download/build size, rendering cost and frame times during implementation. Prefer shared materials, small reusable assets and bounded effects. Record measurements and their limitations; do not promise a frame rate from screenshots.
- Keep only the newest verified game app in unity/PrisonCoop/Builds/macOS. Once a replacement build has passed its relevant checks, run tools/keep-latest-build.py with that bundle name and --apply. Do not delete the last working build before verification. Source, design documents, evidence and devlogs remain.
