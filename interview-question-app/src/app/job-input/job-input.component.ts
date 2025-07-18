import { Component } from '@angular/core';
import { AiService } from '../ai.service';

@Component({
  selector: 'app-job-input',
  templateUrl: './job-input.component.html',
  styleUrls: ['./job-input.component.css']
})
export class JobInputComponent {
  jobDescription: string = '';
  questions: string[] = [];

  constructor(private aiService: AiService) {}

  onSubmit(): void {
    this.aiService.generateQuestions(this.jobDescription).subscribe({
      next: (response) => {
        this.questions = response.questions;
        console.log('Generated Questions:', this.questions);
      },
      error: (err) => {
        console.error('Error generating questions:', err);
      }
    });
  }
}
