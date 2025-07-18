# InterviewQuestions

🚀 **AI-Powered Interview Preparation Tool**

InterviewQuestions is a web application that helps you practice and prepare for job interviews by leveraging OpenAI's GPT model.

## ✨ Features

- 📋 Paste a job description or job application.
- 🤖 Automatically generates **5 tailored interview questions**.
- ✍️ Submit your own answers.
- 🧠 Receive **AI-powered grading and feedback** on each response.

## 🖥️ Tech Stack

- **Front-End:** Angular  
- **Back-End:** .NET Web API (C#)  
- **AI Engine:** OpenAI GPT-3.5 Turbo  
- **Styling:** Custom CSS (responsive, clean design)


## 📦 Installation

### 🔧 Prerequisites

- Node.js & Angular CLI  
- .NET 6 SDK or newer  
- OpenAI API key

### ⚙️ Setup

#### 1. Clone the Repository

```
git clone https://github.com/your-username/interviewquestions.git
cd interviewquestions
```

#### 2. Configure Environment Variables

Create a system environment variable on Windows for the OpenAI API key:

```
setx OPENAI_API_KEY "your-api-key-here"
```

Or add it manually via **System Properties → Environment Variables**.

#### 3. Run the API

Navigate to the `.NET` project:

```
cd InterviewQuestionsAPI
dotnet run
```

#### 4. Run the Angular Front-End

```
cd ../interview-question-app
npm install
ng serve
```

Visit: `http://localhost:4200`

## 🔐 Environment & Security

- Your API key is accessed securely from environment variables.
- CORS and basic error handling are configured on the API side.

## 🧪 Development Notes

- All questions and answers are passed to the API in JSON format.
- The API dynamically prompts GPT to generate questions and later grade answers.
- The front-end uses Angular's reactive forms and `ngModel` binding for input tracking.

## 🤔 Why Use This App?

- Save time preparing for interviews
- Tailor practice to **specific job roles**
- Get actionable feedback instantly
- Improve clarity and confidence before your real interview

## 📜 License

MIT License

---

