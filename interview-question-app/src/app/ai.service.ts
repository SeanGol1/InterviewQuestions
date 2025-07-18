import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AiService {

  private apiUrl = 'https://interviewquestiongenerator-g7bzexepc0beewf9.canadacentral-01.azurewebsites.net/api/openai/';

  constructor(private http: HttpClient) {}

  generateQuestions(jobDescription: string): Observable<any> { 
    return this.http.post<any>(this.apiUrl+'generate-questions', {
      jobDescription: jobDescription
    });
  }

  submitAnswers(data: { question: string; answer: string }[]) {
    return this.http.post(this.apiUrl+'grade-answers', data);
  }
}
