## Welcome to GitHub Pages

You can use the [editor on GitHub](https://github.com/TheRaizer/Azer2DPlatFormer/edit/gh-pages/index.md) to maintain and preview the content for your website in Markdown files.

Whenever you commit to this repository, GitHub Pages will run [Jekyll](https://jekyllrb.com/) to rebuild the pages in your site, from the content in your Markdown files.

### Markdown

Markdown is a lightweight and easy-to-use syntax for styling your writing. It includes conventions for

```markdown
Syntax highlighted code block

# Header 1
## Header 2
### Header 3

- Bulleted
- List

1. Numbered
2. List

**Bold** and _Italic_ and `Code` text

[Link](url) and ![Image](src)
```

For more details see [GitHub Flavored Markdown](https://guides.github.com/features/mastering-markdown/).

### Jekyll Themes

Your Pages site will use the layout and styles from the Jekyll theme you have selected in your [repository settings](https://github.com/TheRaizer/Azer2DPlatFormer/settings). The name of this theme is saved in the Jekyll `_config.yml` configuration file.

### Support or Contact

Having trouble with Pages? Check out our [documentation](https://docs.github.com/categories/github-pages-basics/) or [contact support](https://github.com/contact) and we’ll help you sort it out.

## State Changing When Wall Jumping
{% include youtubePlayer.html id="vvK_GnG-D4c" %}
This video demonstrates the wall jumping mechanic in Azer the Well Kept Lie. Each time the player's animation changes to be attached to the wall you are able to visually see the change from its current state to the wall climb state. When you are in the wall climb state there are certain restrictions put in place, such as being unable to attack or run. These restrictions as well as what to do on enter, exit, and update of the state is managed by a class which inherits from the abstract State class.

```markdown
public abstract class State
    {
        protected StateMachine stateMachine;

        protected State(StateMachine _stateMachine)
        {
            stateMachine = _stateMachine;
        }

        public virtual void Enter()
        {

        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
```

What manages which state is the current state, as well as changing states and running the Enter() and Exit() functions is the StateMachine class. This class is instantiated on different Unity MonoBehaviours that will be utilizing states. Examples are the Player Script and most enemy scripts.

```markdown
public class StateMachine
    {
        public State CurrentState { get; private set; }

        private Dictionary<Type, State> availableStates;

        public void SetStates(Dictionary<Type, State> _availableStates)
        {
            availableStates = _availableStates;
        }

        public void Initialize(Type startingState)
        {
            CurrentState = availableStates[startingState];
            CurrentState.Enter();
        }

        public void ChangeState(Type nextState)
        {
            CurrentState.Exit();
            CurrentState = availableStates[nextState];
            CurrentState.Enter();
        }
    }
```
