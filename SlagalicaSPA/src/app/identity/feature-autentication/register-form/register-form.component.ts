import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent implements OnInit {
  public registerForm: FormGroup;

  constructor(){
    this.registerForm = new FormGroup({
      username: new FormControl('', [Validators.required, Validators.minLength(5)]),
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required, Validators.minLength(5)])
    })
  }

  ngOnInit(): void {
    
  }

  public onRegisterFormSubmit(): void {
    // todo
  }
}
