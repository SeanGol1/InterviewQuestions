import { Component } from '@angular/core';
import { AiService } from '../ai.service';

export interface QuestionAnswer {
  question: string;
  answer: string;
  score?: number;
  feedback?: string;
}

@Component({
  selector: 'app-job-input',
  templateUrl: './job-input.component.html',
  styleUrls: ['./job-input.component.css']
})



export class JobInputComponent {
  jobDescription: string = '';
  loading = false;

  questions: QuestionAnswer[] = [];



  constructor(private aiService: AiService) {}

  onSubmitJob(): void {
    this.loading = true;
    this.aiService.generateQuestions(this.jobDescription).subscribe({
      next: (response) => {
        this.questions = response.questions.map((q: string) => ({ question: q, answer: '' }));
        //this.questions = response.questions;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      },
    });
  }


  onSubmitAnswers() {
    this.loading = true;
  const payload = this.questions.map(q => ({
    question: q.question,
    answer: q.answer || ''
  }));

  this.aiService.submitAnswers(payload).subscribe({
    next: (res : any ) => {
      const graded = res.graded || res.Graded || res;
      this.questions = graded.map((q: QuestionAnswer) => ({ question: q.question, answer: q.answer, score: q.score, feedback:q.feedback }));
      this.loading = false; 

    },
    error: (err) => {
      console.error('Error grading answers:', err);
      this.loading = false;
    }
  });
}



}
