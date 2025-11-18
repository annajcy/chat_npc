# Chat NPC / 智能NPC对话系统

[English](#english) | [中文](#中文)

---

<a name="english"></a>
## 🎮 English

### Overview
An intelligent NPC conversation system powered by Large Language Models (LLM) for Unity. This project enables realistic, AI-driven conversations with NPCs, featuring voice input/output, lip-sync animations, and support for both desktop and WebGL platforms.

![intro](intro.png)

### ✨ Key Features
- 🤖 **LLM Integration**: Powered by Baidu AI APIs for natural language conversations
- 🎤 **Voice Input**: Real-time speech-to-text (STT) conversion
- 🔊 **Voice Output**: Text-to-speech (TTS) with natural voice synthesis
- 👄 **Lip Sync**: Automatic lip synchronization using OVRLipSync
- 🎭 **Facial Expressions**: Dynamic blinking and viseme-based animations
- 🌐 **WebGL Support**: Deployable to web browsers with microphone support
- 🎮 **Third-Person Controller**: Integrated character controller with conversation triggers
- 💬 **Chat UI**: Intuitive chat interface with text and voice input options

### 📋 System Requirements
- **Unity Version**: 2020.3.44f1 or higher (tested on Unity 6000.0.23f1)
- **Operating System**: Windows, macOS, or Linux
- **For WebGL**: Modern web browser with microphone access support
- **API Keys**: Baidu AI API access (for LLM, TTS, STT services)

### 🚀 Installation

1. **Clone the Repository**
   ```bash
   git clone https://github.com/annajcy/chat_npc.git
   cd chat_npc
   ```

2. **Open in Unity**
   - Launch Unity Hub
   - Click "Add" and select the cloned project folder
   - Open with Unity 2020.3.44f1 or higher

3. **Install Dependencies**
   - The project uses Unity Package Manager
   - Required packages will be automatically installed:
     - Unity Input System
     - TextMesh Pro
     - Cinemachine
     - Animation Rigging

### ⚙️ Configuration

#### 1. Baidu AI API Setup
To use the conversation system, you need to configure Baidu AI API credentials:

1. **Get API Access**
   - Visit [Baidu AI Cloud](https://cloud.baidu.com/)
   - Register and create an application
   - Obtain your `Access Token` for:
     - ERNIE Bot (LLM)
     - Text-to-Speech (TTS)
     - Speech-to-Text (STT)

2. **Configure in Unity**
   - Open the main scene: `Assets/Scenes/chat_code_scene.unity`
   - Find the `ChatCore` GameObject
   - In the Inspector, set:
     - **LLM**: Configure URL and Access Token
     - **TTS**: Configure URL and Access Token
     - **STT**: Configure URL and Access Token

#### 2. Model Selection
The project supports multiple Baidu LLM models:
- Qianfan-Chinese-Llama-2-7B
- ERNIE-Bot
- ERNIE-Bot-turbo
- Other Baidu AI models

Select your preferred model in the `ChatBaidu` component.

### 🎯 Usage

#### Desktop Mode
1. Open the scene `Assets/Scenes/chat_code_scene.unity`
2. Press Play in Unity Editor
3. Use WASD to move the character
4. Approach an NPC and press **C** to start conversation
5. Choose input method:
   - **Text Input**: Type your message and press Enter
   - **Voice Input**: Click the microphone button and speak
6. Press **ESC** to exit conversation mode

#### WebGL Deployment
1. **Build Settings**
   - Go to File → Build Settings
   - Select WebGL platform
   - Configure Player Settings:
     - Color Space: `Gamma`
     - Decompression Fallback: `Enabled`

2. **Build the Project**
   - Ensure project path contains only English characters
   - Click "Build" and select output folder

3. **Configure JavaScript**
   - Copy `Assets/Tool/Webgl/webglVoiceInput/Plugins/JavaScripts/recorder.wav.min.js` to build folder
   - Modify `index.html` following instructions in `Assets/Tool/Webgl/webglVoiceInput/README.md`

4. **Deploy**
   - Use a local web server (e.g., PhpStudy, Node.js http-server)
   - Access via `http://localhost` in your browser

### 📁 Project Structure
```
Assets/
├── Animation/          # Character animations
├── Mesh/              # 3D models and meshes
├── Scenes/            # Unity scenes
│   ├── chat_code_scene.unity    # Main conversation scene
│   └── SampleScene.unity
├── Scripts/           # C# scripts
│   ├── Chat/          # Conversation logic
│   │   ├── ChatCore.cs
│   │   ├── ConversationController.cs
│   │   └── VoiceInputs.cs
│   ├── Expression/    # Facial animation
│   │   ├── AudioToLip.cs
│   │   └── BlinkController.cs
│   ├── Framework/     # Core framework
│   ├── Infrasturcture/ # API integrations
│   │   ├── LLM/       # Language model
│   │   ├── TTS/       # Text-to-speech
│   │   └── STT/       # Speech-to-text
│   └── Panels/        # UI panels
├── Settings/          # Project settings
├── Starter Assets/    # Third-person controller
├── TextMesh Pro/      # Text rendering
├── Tool/              # Utilities
│   ├── LipSync/       # OVRLipSync integration
│   └── Webgl/         # WebGL voice input support
└── Texure/            # Textures and materials
```

### 🛠️ Technology Stack
- **Game Engine**: Unity (6000.0.23f1)
- **Programming Language**: C#
- **LLM Provider**: Baidu AI (ERNIE Bot)
- **TTS/STT**: Baidu AI Speech Services
- **Lip Sync**: OVRLipSync
- **Character Controller**: Unity Starter Assets
- **UI Framework**: Unity UI with TextMesh Pro
- **Input System**: Unity Input System

### 🌐 WebGL Notes
- WebGL builds require special handling for microphone access
- The project includes custom JavaScript integration for audio recording
- Detailed setup instructions: `Assets/Tool/Webgl/webglVoiceInput/README.md`
- Ensure HTTPS or localhost for microphone permissions

### 🤝 Contributing
Contributions are welcome! Please feel free to submit issues or pull requests.

### 📄 License
This project is open source. Please check the repository for license details.

### 📞 Contact & Credits
- **Author**: annajcy
- **WebGL Voice Solution**: Based on work by @阴沉的怪咖
- **References**: 
  - [Unity WebGL Scripting](https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html)
  - [UnityWebGLMicrophone](https://github.com/HiWenHao/UnityWebGLMicrophone)

---

<a name="中文"></a>
## 🎮 中文

### 项目简介
这是一个基于大型语言模型(LLM)的Unity智能NPC对话系统。该项目实现了与NPC的真实AI驱动对话，支持语音输入输出、口型同步动画，并支持桌面端和WebGL平台。

![intro](intro.png)

### ✨ 主要特性
- 🤖 **LLM集成**：基于百度AI API实现自然语言对话
- 🎤 **语音输入**：实时语音转文字(STT)功能
- 🔊 **语音输出**：文字转语音(TTS)自然语音合成
- 👄 **口型同步**：使用OVRLipSync自动口型同步
- 🎭 **面部表情**：动态眨眼和基于音素的动画
- 🌐 **WebGL支持**：可部署到Web浏览器，支持麦克风
- 🎮 **第三人称控制器**：集成角色控制器和对话触发
- 💬 **聊天界面**：直观的聊天UI，支持文字和语音输入

### 📋 系统要求
- **Unity版本**：2020.3.44f1或更高（在Unity 6000.0.23f1上测试）
- **操作系统**：Windows、macOS或Linux
- **WebGL部署**：支持麦克风访问的现代浏览器
- **API密钥**：百度AI API访问权限（用于LLM、TTS、STT服务）

### 🚀 安装步骤

1. **克隆仓库**
   ```bash
   git clone https://github.com/annajcy/chat_npc.git
   cd chat_npc
   ```

2. **在Unity中打开**
   - 启动Unity Hub
   - 点击"添加"并选择克隆的项目文件夹
   - 使用Unity 2020.3.44f1或更高版本打开

3. **安装依赖**
   - 项目使用Unity Package Manager
   - 所需包将自动安装：
     - Unity Input System
     - TextMesh Pro
     - Cinemachine
     - Animation Rigging

### ⚙️ 配置说明

#### 1. 百度AI API设置
要使用对话系统，需要配置百度AI API凭据：

1. **获取API访问权限**
   - 访问[百度智能云](https://cloud.baidu.com/)
   - 注册并创建应用
   - 获取以下服务的`Access Token`：
     - 文心一言（LLM）
     - 文字转语音（TTS）
     - 语音识别（STT）

2. **在Unity中配置**
   - 打开主场景：`Assets/Scenes/chat_code_scene.unity`
   - 找到`ChatCore`游戏对象
   - 在Inspector中设置：
     - **LLM**：配置URL和Access Token
     - **TTS**：配置URL和Access Token
     - **STT**：配置URL和Access Token

#### 2. 模型选择
项目支持多个百度LLM模型：
- 千帆-Chinese-Llama-2-7B
- 文心一言
- 文心一言-turbo
- 其他百度AI模型

在`ChatBaidu`组件中选择您偏好的模型。

### 🎯 使用方法

#### 桌面端模式
1. 打开场景`Assets/Scenes/chat_code_scene.unity`
2. 在Unity编辑器中点击播放
3. 使用WASD移动角色
4. 接近NPC并按**C键**开始对话
5. 选择输入方式：
   - **文字输入**：输入消息并按回车
   - **语音输入**：点击麦克风按钮并说话
6. 按**ESC键**退出对话模式

#### WebGL部署
1. **构建设置**
   - 前往 文件 → 构建设置
   - 选择WebGL平台
   - 配置Player Settings：
     - Color Space：`Gamma`
     - Decompression Fallback：`启用`

2. **构建项目**
   - 确保项目路径仅包含英文字符
   - 点击"构建"并选择输出文件夹

3. **配置JavaScript**
   - 将`Assets/Tool/Webgl/webglVoiceInput/Plugins/JavaScripts/recorder.wav.min.js`复制到构建文件夹
   - 按照`Assets/Tool/Webgl/webglVoiceInput/README.md`中的说明修改`index.html`

4. **部署**
   - 使用本地Web服务器（如PhpStudy、Node.js http-server）
   - 通过浏览器访问`http://localhost`

### 📁 项目结构
```
Assets/
├── Animation/          # 角色动画
├── Mesh/              # 3D模型和网格
├── Scenes/            # Unity场景
│   ├── chat_code_scene.unity    # 主对话场景
│   └── SampleScene.unity
├── Scripts/           # C#脚本
│   ├── Chat/          # 对话逻辑
│   │   ├── ChatCore.cs
│   │   ├── ConversationController.cs
│   │   └── VoiceInputs.cs
│   ├── Expression/    # 面部动画
│   │   ├── AudioToLip.cs
│   │   └── BlinkController.cs
│   ├── Framework/     # 核心框架
│   ├── Infrasturcture/ # API集成
│   │   ├── LLM/       # 语言模型
│   │   ├── TTS/       # 文字转语音
│   │   └── STT/       # 语音转文字
│   └── Panels/        # UI面板
├── Settings/          # 项目设置
├── Starter Assets/    # 第三人称控制器
├── TextMesh Pro/      # 文本渲染
├── Tool/              # 工具
│   ├── LipSync/       # OVRLipSync集成
│   └── Webgl/         # WebGL语音输入支持
└── Texure/            # 纹理和材质
```

### 🛠️ 技术栈
- **游戏引擎**：Unity（6000.0.23f1）
- **编程语言**：C#
- **LLM提供商**：百度AI（文心一言）
- **TTS/STT**：百度AI语音服务
- **口型同步**：OVRLipSync
- **角色控制器**：Unity Starter Assets
- **UI框架**：Unity UI + TextMesh Pro
- **输入系统**：Unity Input System

### 🌐 WebGL注意事项
- WebGL构建需要特殊处理麦克风访问
- 项目包含用于音频录制的自定义JavaScript集成
- 详细设置说明：`Assets/Tool/Webgl/webglVoiceInput/README.md`
- 确保使用HTTPS或localhost以获得麦克风权限

### 🤝 贡献
欢迎贡献！请随时提交问题或拉取请求。

### 📄 许可证
本项目是开源的。请查看仓库了解许可证详情。

### 📞 联系方式与致谢
- **作者**：annajcy
- **WebGL语音解决方案**：基于@阴沉的怪咖的工作
- **参考资料**：
  - [Unity WebGL脚本交互](https://docs.unity3d.com/cn/Manual/webgl-interactingwithbrowserscripting.html)
  - [UnityWebGLMicrophone](https://github.com/HiWenHao/UnityWebGLMicrophone)
