import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AutenticationFacadeService } from '../../domain/aplication-services/autentication-facade.service';
import { Router } from '@angular/router';

interface IRegisterFormData {
  username: string,
  password: string,
  firstName: string,
  lastName: string,
  email: string

}

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent implements OnInit {
  public registerForm: FormGroup;

  constructor(private authenicationService: AutenticationFacadeService,
    private routerService: Router){
    this.registerForm = new FormGroup({
      username: new FormControl('test1', [Validators.required, Validators.minLength(5)]),
      firstName: new FormControl('y', [Validators.required]),
      lastName: new FormControl('z', [Validators.required]),
      password: new FormControl('12345678', [Validators.required, Validators.minLength(5)]),
      email: new FormControl('t@test', [Validators.email, Validators.required])
    })
  }

  ngOnInit(): void {
    
  }

  public onRegisterFormSubmit(): void {
    if(this.registerForm.invalid) {
      window.alert('Username and password need to have more than 5 characters, and all the fields are required.');
      return;
    }  
  
    const data: IRegisterFormData = this.registerForm.value as IRegisterFormData;

    this.authenicationService.register(
      data.username, data.password, data.email, data.firstName, data.lastName).subscribe(
      (success: boolean) => {
        if(success) {
          this.registerForm.reset();
          this.routerService.navigate(['/profile']);
        } else {
          window.alert('Wrong credentials. Try again!')
        }
      }
    );   }
}
