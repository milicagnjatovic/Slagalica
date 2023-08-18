import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AutenticationFacadeService } from '../../domain/aplication-services/autentication-facade.service';
import { Router } from '@angular/router';

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

  constructor(private authenicationService: AutenticationFacadeService,
    private routerService: Router){
    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required, Validators.minLength(5)]),
      password: new FormControl('', [Validators.required, Validators.minLength(5)])

    })
  }

  ngOnInit(): void {
    
  }

  public onLoginFormSubmit(): void {
    if(this.loginForm.invalid) {
      window.alert('Invalid data');
      return;
    }

    const data: ILoginFormData = this.loginForm.value as ILoginFormData;

console.log('Data1 ')
console.log(this.loginForm)
console.log(this.loginForm.value)
console.log(this.loginForm.value as ILoginFormData)

    this.authenicationService.login(data.username, data.password).subscribe(
      (success: boolean) => {
        window.alert('logged in' + success + data.username + data.password);
        this.loginForm.reset();
        if(success) {
          this.routerService.navigate(['/profile']);
        }
      }
    ); 
  }

}
