import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AutenticationFacadeService } from '../../domain/aplication-services/autentication-facade.service';

interface ILoginFormData {
  username: string;
  password: string
};


@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})

export class LoginFormComponent  implements OnInit{
  public loginForm: FormGroup;

  constructor(private authenicationService: AutenticationFacadeService){
    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required, Validators.minLength(5)]),
      password: new FormControl('', [Validators.required, Validators.minLength(5)])

    })
  }

  ngOnInit(): void {
    
  }

  public onLoginFormSubmit(): void {
    if(this.loginForm.invalid) {
      window.alert('Error');
      return;
    }

    const data: ILoginFormData = this.loginForm.value as ILoginFormData;

    this.authenicationService.login(data.username, data.username).subscribe(
      (success: boolean) => {
        window.alert('logged in');
        this.loginForm.reset();
      }
    ); 
  }

}
