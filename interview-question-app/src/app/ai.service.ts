import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AiService {

  private apiUrl = 'https://localhost:5001/api/openai/generate-questions';

  constructor(private http: HttpClient) {}

  generateQuestions(jobDescription: string): Observable<any> { 
    return this.http.post<any>('https://localhost:7052/api/openai/generate-questions', {
      jobDescription: jobDescription
    });
  }
}
