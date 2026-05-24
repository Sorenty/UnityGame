# UnityGame - Survival Island 🏝️

A 2D multiplayer survival game built with **Unity** and **Mirror** networking framework, featuring cooperative gameplay and XR basketball minigame.

**Languages:** English | [Русский](#-описание-на-русском)

---

## 📖 Overview

**Survival Island** is an engaging 2D survival game where players must work together (in multiplayer mode) to find an axe, chop trees, gather resources, and repair a boat to escape the island. The game includes an interactive XR basketball scene for additional gameplay variety.

### Key Features:
- 🎮 **Single-player Survival Mode**: Complete missions solo with local AI assistance
- 👥 **Multiplayer Mode**: Play cooperatively with other players via Mirror networking
- 🪓 **Resource Management**: Find an axe and chop trees to gather wood
- 🚤 **Boat Repair Mechanic**: Collect resources to progressively repair the boat
- 🏀 **XR Basketball Mini-game**: VR/XR support with ball-throwing target game
- 🎨 **2D Pixel Art Graphics**: Charming retro visual style
- 🔊 **Dynamic Audio System**: Background music and sound effects management

---

## 🎮 Gameplay Mechanics

### Main Objective
Survive on the island by gathering resources and repairing the boat to escape.

### Resource System
| Resource | How to Get | Purpose |
|----------|-----------|---------|
| **Wood** (Tree) | Chop trees with axe | Repair boat (need 20+ pieces) |
| **Ladder Parts** (Lestva) | Find around the island | Assist in boat repair (need 10+ pieces) |
| **Axe** | Located somewhere on the island | Required to chop trees |
| **Health Packs** | Spawn throughout the map | Restore player health (max 250 HP) |

### Progression States
1. **Early Game**: Search for the axe on the island
2. **Mid Game**: Chop trees to collect wood
3. **Late Game**: Gather ladder parts
4. **Victory**: Repair boat fully and escape

### Boat Status Indicators
- 🔴 **Stage 1**: Broken boat (starting state)
- 🟡 **Stage 2**: Wood collected (10+ tree resources)
- 🟢 **Stage 3**: Fully repaired (20+ wood + 10+ ladder parts) → **WIN!**

---

## 🕹️ Controls

### Single-player/Local Game
| Action | Key |
|--------|-----|
| Move Left | `A` or `Left Arrow` |
| Move Right | `D` or `Right Arrow` |
| Jump | `Spacebar` |
| Interact/Use | `E` |
| Pause | `ESC` |

### Multiplayer (Network)
- Same controls as above
- All player actions are synchronized across the network
- Resource collection is shared (all players benefit from collected resources)

### XR Basketball
- **Throw Ball**: Trigger button
- **Aim**: Head tracking / Controller direction
- **Score**: Direct hits into the basket (multiple hits possible)

---

## 🌐 Multiplayer Features

### Architecture
- **Networking Library**: Mirror
- **Transport Protocol**: Telepathy (TCP-based)
- **Synchronization**: Real-time player positions and resource updates
- **Scene Management**: Automatic scene syncing between server and clients

### How to Play Multiplayer

#### Start as Server (Host):
1. Launch game and navigate to Multiplayer menu
2. Click "Start Host"
3. Share connection details with other players

#### Connect as Client:
1. Launch game and navigate to Multiplayer menu
2. Click "Connect to Server"
3. Enter server IP address
4. Click Connect

#### Network Features:
- ✅ Synchronized player movement and animations
- ✅ Shared resource collection
- ✅ Real-time health updates
- ✅ Synchronized boat repair progress
- ✅ Automatic disconnection recovery

---

## 🥽 XR Features

### XR Basketball Scene (`XR_Basketball.unity`)

A dedicated scene featuring a basketball mini-game with XR support:

**Features:**
- Ball throwing mechanics
- Basket target with collision detection
- Score tracking system
- Compatible with VR/XR controllers
- Physics-based ball movement

**Access:**
- Available from main menu under "XR Basketball" or "Mini-games"
- Supports both traditional input and VR controllers
- Real-time score display

---

## 🛠️ Technical Details

### Project Structure

```
Assets/
├── Scenes/
│   ├── 1.unity                    # Main game scene
│   ├── menu.unity                 # Main menu
│   ├── Multiplayer_01.unity       # Multiplayer game scene
│   └── XR_Basketball.unity        # XR mini-game scene
├── Scripts/
│   ├── GameSettings.cs            # Global game configuration
│   ├── PlayerStats.cs             # Player stats (synced over network)
│   ├── NetworkPlayerController.cs # Multiplayer player controller
│   ├── BoatChange.cs              # Boat repair progression
│   ├── HealthChange.cs            # Health UI management
│   ├── BasketScoreZone.cs         # XR basketball scoring
│   ├── ItemSpawner.cs             # Health pack spawning
│   ├── AudioManager.cs            # Audio system
│   └── ... (other utility scripts)
├── Prefabs/
│   ├── aptechka.prefab            # Health pack prefab
│   └── ... (other prefabs)
├── Mirror/                        # Mirror networking framework
├── XR/                            # XR interaction toolkit
├── Audio/                         # Sound effects and music
└── ...
```

### Network Synchronization

**Synced Variables** (in `PlayerStats.cs`):
```csharp
[SyncVar] public int health;        // Player health
[SyncVar] public int tree;          // Wood collected
[SyncVar] public int lestva;        // Ladder parts collected
[SyncVar] public bool haveAxe;      // Has axe
[SyncVar] public int num_of_eat_hp; // Health packs used
```

**Network Commands**:
- `CmdAddHealth()` - Restore health
- `CmdAddTree()` - Add wood
- `CmdAddLestva()` - Add ladder parts
- `CmdSetHaveAxe()` - Set axe status
- `CmdTakeDamage()` - Take damage

### Health System
- **Max Health**: 250 HP
- **Health States** (UI indicator):
  - 200+ HP: Full health (green)
  - 150-199 HP: Good health (light green)
  - 100-149 HP: Medium health (yellow)
  - 50-99 HP: Low health (orange)
  - 1-49 HP: Critical health (red)
  - 0 HP: Dead (skull icon) → Game Over

---

## 📋 Requirements

### Minimum Requirements
- **Unity Version**: 2020.3 LTS or newer
- **Platform**: Windows, macOS, Linux, WebGL (with limitations)
- **RAM**: 4 GB
- **GPU**: Any GPU supporting DirectX 11 / OpenGL 3.0+

### Optional (for XR)
- **VR Headset**: Meta Quest 3/Pro, HTC Vive, Valve Index, etc.
- **XR Hands Package**: For hand tracking support

### Dependencies
- Mirror (v64.0 or later)
- TextMesh Pro
- Unity Input System
- Universal Render Pipeline (URP)
- XR Hands Samples (optional)

---

## ⚙️ Installation & Setup

### 1. Clone Repository
```bash
git clone https://github.com/yourusername/UnityGame-main.git
cd UnityGame-main
```

### 2. Open in Unity
1. Open Unity Hub
2. Click "Open Project"
3. Select the project folder
4. Wait for Unity to import assets (may take a few minutes)

### 3. Configure Network Settings (for Multiplayer)
1. Open `Assets/Mirror/` folder
2. Locate `NetworkManager` in the scene
3. Configure transport settings:
   - **Port**: Default is 7777
   - **Max Connections**: Adjust based on desired player count
   - **Network Address**: Set server IP for clients

### 4. Build & Run

**Single Player:**
```
File > Build and Run (or Ctrl+B)
```

**Multiplayer (Testing in Editor):**
1. Go to `Window > ParrelSync > Create Clones` (for second instance)
2. Run both instances (one as host, one as client)

**For Production:**
```
File > Build Settings
- Add scenes: menu.unity, Multiplayer_01.unity, XR_Basketball.unity
- Select target platform
- Click "Build"
```

---

## 🎯 Gameplay Tips

### Tips for Beginners
1. **Explore thoroughly** - The axe isn't where you'd expect!
2. **Manage health** - Collect health packs early
3. **Work efficiently** - Each tree gives 1 wood unit
4. **Team up** - Multiplayer makes resource gathering faster
5. **Watch the boat** - It changes visually as you progress

### Speedrun Strategy
1. Find axe location → Chop 20 trees → Collect 10 ladder parts → Repair boat
2. Coordinate with teammates to cover more ground
3. Prioritize health packs if available

### XR Basketball Tricks
- Throw at an angle for bank shots
- Multiple balls can score simultaneously
- Consecutive hits earn combo points

---

## 🐛 Known Issues & Troubleshooting

### Connection Issues
- **Problem**: Can't connect to server
  - **Solution**: Check firewall, ensure port 7777 is open, verify IP address

### Physics Glitches
- **Problem**: Player falling through map
  - **Solution**: Verify collider components, rebuild scene physics

### Performance Issues
- **Problem**: Low FPS in multiplayer
  - **Solution**: Reduce player count, lower graphics settings, update graphics drivers

### Audio Not Playing
- **Problem**: No sound effects/music
  - **Solution**: Check AudioManager is in scene, verify volume settings

---

## 📝 Project Credits

- **Engine**: Unity
- **Networking**: Mirror by vis2k
- **Art**: Pixel art assets (original + community contributions)
- **Development**: ИКБО-Мирэа Team

---

## 📄 License

This project is developed as an educational game project for ИКБО-Мирэа.

---

## 🤝 Contributing

Contributions are welcome! Please:
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/NewFeature`)
3. Commit changes (`git commit -m 'Add new feature'`)
4. Push to branch (`git push origin feature/NewFeature`)
5. Open a Pull Request

---

## 📧 Support & Contact

For bugs, questions, or suggestions:
- Create an Issue in GitHub
- Contact: https://github.com/Sorenty/UnityGame

---

---

# 🇷🇺 Описание на русском

## Обзор

**Survival Island** - это увлекательная 2D игра выживания, где игроки должны найти топор, рубить деревья, собирать ресурсы и починить лодку, чтобы покинуть остров. Игра поддерживает кооперативный мультиплеер через Mirror и включает мини-игру XR баскетбол.

### Основные возможности:
- 🎮 Режим выживания для одного игрока
- 👥 Кооперативный мультиплеер
- 🪓 Система управления ресурсами
- 🚤 Механика восстановления лодки
- 🏀 Мини-игра баскетбол для XR
- 🎨 2D пиксель-арт графика
- 🔊 Система динамического звука

## 🎮 Механика Игры

### Основная Цель
Выжить на острове, собирая ресурсы и починив лодку.

### Система Ресурсов
| Ресурс | Как получить | Назначение |
|--------|-----------|-----------|
| **Дерево** | Рубить деревья топором | Починить лодку (нужно 20+) |
| **Лестница** (Lestva) | Найти на острове | Помощь в ремонте лодки (нужно 10+) |
| **Топор** | Найти на острове | Необходим для рубки деревьев |
| **Аптечки** | Появляются на карте | Восстановить здоровье (макс 250 HP) |

### Стадии Прогресса
1. **Ранняя игра**: Поиск топора
2. **Середина игры**: Рубка деревьев
3. **Поздняя игра**: Сбор частей лестницы
4. **Победа**: Полный ремонт лодки и уход с острова

## 🕹️ Управление

| Действие | Клавиша |
|----------|----------|
| Влево | `A` или `←` |
| Вправо | `D` или `→` |
| Прыжок | `Пробел` |
| Взаимодействие | `E` |
| Пауза | `ESC` |

## 🌐 Мультиплеер

### Запуск Сервера (Хост)
1. Запустите игру
2. Перейдите в меню Мультиплеер
3. Нажмите "Start Host"
4. Поделитесь адресом сервера

### Подключение как Клиент
1. Запустите игру
2. Перейдите в меню Мультиплеер
3. Нажмите "Connect to Server"
4. Введите IP адрес сервера

## 🛠️ Требования

- **Unity**: 2020.3 LTS или новее
- **ОС**: Windows, macOS, Linux
- **ОЗУ**: 4 GB минимум
- **Интернет**: Для мультиплеера

## ⚙️ Установка

### 1. Клонируйте репозиторий
```bash
git clone https://github.com/Sorenty/UnityGame-main
cd UnityGame-main
```

### 2. Откройте в Unity
- Откройте Unity Hub
- Нажмите "Open Project"
- Выберите папку проекта
- Ждите импорта ассетов

### 3. Запуск
```
File > Build and Run (Ctrl+B)
```

## 🎯 Советы для Игроков

1. **Исследуйте**: Топор спрятан в необычном месте!
2. **Экономьте здоровье**: Собирайте аптечки
3. **Работайте эффективно**: Каждое дерево = 1 единица дерева
4. **Играйте вместе**: Мультиплеер ускоряет сбор ресурсов
5. **Следите за лодкой**: Она меняется визуально при прогрессе

## 📞 Контакты и Поддержка

Вопросы и предложения:
- Создавайте Issues на GitHub
- Контакт: https://github.com/Sorenty/UnityGame

---

Enjoy the game! 🎮 Удачи в игре! 🎮
