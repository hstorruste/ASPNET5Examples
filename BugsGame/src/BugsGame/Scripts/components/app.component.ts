import {Component} from 'angular2/core';
import {LevelComponent} from "./level.component";

@Component({
    selector: 'app',
    template: `'Hello World!'
    <br>
    This is the app component
    <level></level>
`,
    directives: [LevelComponent]
})
export class AppComponent {

}